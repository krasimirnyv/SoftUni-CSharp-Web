namespace BookVerse.Services.Core.Contracts;

using ViewModels.Book;

public interface IBookService
{
    Task<IEnumerable<BookIndexViewModel>> GetAllBooksAsync(string? userId);
    
    Task<BookDetailsViewModel?> GetBookDetailsByIdAsync(int bookId, string? userId);
    
    Task<BookFormModel> GetBookCreateViewModelAsync();
    
    Task AddBookAsync(BookFormModel model, string publisherId);
    
    Task<IEnumerable<BookUserFavouriteBooksViewModel>> GetUserFavoriteBooksAsync(string userId);

    Task AddToMyFavoriteBooksAsync(int bookId, string userId);

    Task RemoveFromMyFavouriteBooksAsync(int bookId, string userId);
    
    Task<BookEditInputModel?> GetBookForEditAsync(int bookId, string userId);

    Task EditBookAsync(BookEditInputModel model, string userId);
    
    Task<BookDeleteViewModel?> GetBookDeleteDetailsAsync(int bookId);
    
    Task DeleteBookAsync(int bookId);
    
    Task<bool> IsBookExistByIsbnAsync(string isbn);
    
    Task<bool> IsBookExistByIdAsync(int bookId);
    
    Task<bool> IsBookSavedAsync(int bookId, string userId);

    Task<bool> IsBookPublisherAsync(int bookId, string userId);
}