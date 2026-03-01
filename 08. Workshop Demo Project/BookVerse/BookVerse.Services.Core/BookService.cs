namespace BookVerse.Services.Core;

using System.Globalization;

using Data;
using DataModels;

using Contracts;

using ViewModels.Book;
using ViewModels.Genre;

using static GCommon.ApplicationConstants;

using Microsoft.EntityFrameworkCore;

public class BookService(BookVerseDbContext context) : IBookService
{
    public async Task<IEnumerable<BookIndexViewModel>> GetAllBooksAsync(string? userId)
    {
        IEnumerable<BookIndexViewModel> allBooks = await context
            .Books
            .Include(b => b.Genre)
            .Include(b => b.UsersBooks)
            .AsNoTracking()
            .AsSplitQuery()
            .Select(b => new BookIndexViewModel
            {
                Id = b.Id,
                Title = b.Title,
                CoverImageUrl = b.CoverImageUrl,
                GenreName = b.Genre.Name,
                SavedCount = b.UsersBooks.Count,
                IsAuthor = userId != null && b.PublisherId.ToLower() == userId.ToLower(),
                IsSaved = userId != null && b.UsersBooks.Any(ub => ub.UserId.ToLower() == userId.ToLower())
            })
            .OrderBy(b => b.Title)
            .ThenBy(b => b.GenreName)
            .ThenByDescending(b => b.SavedCount)
            .ToArrayAsync();

        return allBooks;
    }
    
    public async Task<BookDetailsViewModel?> GetBookDetailsByIdAsync(int bookId, string? userId)
    {
        BookDetailsViewModel? bookDetails = await context
            .Books
            .Include(b => b.Genre)
            .Include(b => b.Publisher)
            .Include(b => b.UsersBooks)
            .AsNoTracking()
            .Where(b => b.Id == bookId)
            .Select(b => new BookDetailsViewModel
            {
                Id = b.Id,
                Title = b.Title,
                CoverImageUrl = b.CoverImageUrl,
                GenreName = b.Genre.Name,
                SavedCount = b.UsersBooks.Count,
                IsAuthor = userId != null && b.PublisherId.ToLower() == userId.ToLower(),
                IsSaved = userId != null && b.UsersBooks.Any(ub => ub.UserId.ToLower() == userId.ToLower()),
                Description = b.Description,
                PublishedOn = b.PublishedOn.ToString(DateFormat, CultureInfo.InvariantCulture),
                Publisher = b.Publisher.UserName!
            })
            .SingleOrDefaultAsync();
        
        return bookDetails;
    }
    
    public async Task<BookFormModel> GetBookCreateViewModelAsync()
    {
        BookFormModel formModel = new BookFormModel
        {
            Genres = await context
                .Genres
                .AsNoTracking()
                .Select(g => new GenreViewModel
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .OrderBy(g => g.Name)
                .ToArrayAsync()
        };

        return formModel;
    }
    
    public async Task AddBookAsync(BookFormModel model, string publisherId)
    {
        Book newBook = new Book
        {
            Isbn = model.Isbn,
            Title = model.Title,
            Description = model.Description,
            CoverImageUrl = model.CoverImageUrl,
            GenreId = model.GenreId,
            PublisherId = publisherId,
            PublishedOn = DateOnly.FromDateTime(model.PublishedOn)
        };
        
        await context.Books.AddAsync(newBook);
        await context.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<BookUserFavouriteBooksViewModel>> GetUserFavoriteBooksAsync(string userId)
    {
        IEnumerable<BookUserFavouriteBooksViewModel> allFavouriteBooks = await context
            .Books
            .Include(b => b.Genre)
            .Include(b => b.UsersBooks)
            .Where(b => b.UsersBooks.Any(ub => ub.UserId.ToLower() == userId.ToLower()))
            .Select(b => new BookUserFavouriteBooksViewModel
            {
                Id = b.Id,
                Title = b.Title,
                CoverImageUrl = b.CoverImageUrl,
                GenreName = b.Genre.Name,
            })
            .OrderBy(b => b.Title)
            .ThenBy(b => b.GenreName)
            .ToArrayAsync();
        
        return allFavouriteBooks;
    }

    public async Task AddToMyFavoriteBooksAsync(int bookId, string userId)
    {
        Book? book = await context
            .Books
            .FindAsync(bookId);

        if (book is not null)
        {
            UserBook newUserBook = new UserBook
            {
                BookId = bookId,
                UserId = userId
            };
            
            await context.UsersBooks.AddAsync(newUserBook);
            await context.SaveChangesAsync();
        }
    }

    public async Task RemoveFromMyFavouriteBooksAsync(int bookId, string userId)
    { 
        UserBook? userBookToDelete = await context
            .UsersBooks
            .AsNoTracking()
            .SingleOrDefaultAsync(ub => ub.UserId.ToLower() == userId.ToLower() && ub.BookId == bookId);
        
        if (userBookToDelete is not null)
        { 
            context.UsersBooks.Remove(userBookToDelete);
            await context.SaveChangesAsync();
        }
    }

    public async Task<BookEditInputModel?> GetBookForEditAsync(int bookId, string userId)
    {
        BookEditInputModel? bookDeleteViewModel = await context
            .Books
            .Include(b => b.Genre)
            .Where(b => b.Id == bookId)
            .Select(b => new BookEditInputModel
            { 
                Id = b.Id,
                Isbn = b.Isbn,
                Title = b.Title,
                Description = b.Description,
                CoverImageUrl = b.CoverImageUrl,
                PublishedOn = b.PublishedOn.ToDateTime(TimeOnly.MinValue),
                GenreId = b.GenreId,
                GenreName = b.Genre.Name,
            })
            .SingleOrDefaultAsync();
        
        return bookDeleteViewModel;
    }

    public async Task EditBookAsync(BookEditInputModel model, string userId)
    {
        Book? bookToEdit = await context
            .Books
            .FindAsync(model.Id);

        if (bookToEdit is not null)
        {
            bookToEdit.Isbn = model.Isbn;
            bookToEdit.Title = model.Title;
            bookToEdit.Description = model.Description;
            bookToEdit.CoverImageUrl = model.CoverImageUrl;
            bookToEdit.GenreId = model.GenreId;
            bookToEdit.PublishedOn = DateOnly.FromDateTime(model.PublishedOn);
            
            await context.SaveChangesAsync();
        }
    }

    public async Task<BookDeleteViewModel?> GetBookDeleteDetailsAsync(int bookId)
    {
        BookDeleteViewModel? bookDeleteViewModel = await context
            .Books
            .Include(b => b.Publisher)
            .AsNoTracking()
            .Where(b => b.Id == bookId)
            .Select(b => new BookDeleteViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Publisher = b.Publisher.UserName!
            })
            .SingleOrDefaultAsync();
        
        return bookDeleteViewModel;
    }

    public async Task DeleteBookAsync(int bookId)
    {
        Book? bookToDelete = await context
            .Books
            .FindAsync(bookId);

        if (bookToDelete is not null)
        {
            bookToDelete.IsDeleted = true;
            await context.SaveChangesAsync();
        }
    }

    public async Task<bool> IsBookExistByIsbnAsync(string isbn)
    {
        bool bookExists = await context
            .Books
            .AsNoTracking()
            .AnyAsync(b => b.Isbn.ToLower() == isbn.ToLower());
        
        return bookExists;
    }

    public async Task<bool> IsBookExistByIdAsync(int bookId)
    {
        bool bookExists = await context
            .Books
            .AsNoTracking()
            .AnyAsync(b => b.Id == bookId);
        
        return bookExists;
    }

    public async Task<bool> IsBookSavedAsync(int bookId, string userId)
    {
        bool isBookAlreadySaved = false;

        if (!string.IsNullOrEmpty(userId))
        {
            isBookAlreadySaved = await context
                .UsersBooks
                .AsNoTracking()
                .AnyAsync(ub => ub.UserId.ToLower() == userId.ToLower() && ub.BookId == bookId);
        }
        
        return isBookAlreadySaved;
    }

    public async Task<bool> IsBookPublisherAsync(int bookId, string userId)
    {
        bool isPublisher = false;

        if (!string.IsNullOrEmpty(userId))
        {
            isPublisher = await context
                .Books
                .AsNoTracking()
                .AnyAsync(b => b.Id == bookId && b.PublisherId.ToLower() == userId.ToLower());
        }
        
        return isPublisher;
    }
}