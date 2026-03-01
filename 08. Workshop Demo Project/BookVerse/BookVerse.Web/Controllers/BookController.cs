namespace BookVerse.Web.Controllers;

using Services.Core.Contracts;
using ViewModels.Book;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class BookController(IBookService bookService, IGenreService genreService, ILogger<BookController> logger) : BaseController
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        string? userId = GetUserId();
        
        IEnumerable<BookIndexViewModel> books = await bookService.GetAllBooksAsync(userId);
        return View(books);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        string? userId = GetUserId();
        BookDetailsViewModel? bookDetails = await bookService.GetBookDetailsByIdAsync(id, userId);

        if (bookDetails is null)
        {
            TempData["ErrorMessage"] = "The requested book does not exist!";
            return RedirectToAction("Index");
        }
        
        return View(bookDetails);
    }
    
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        BookFormModel formModel = await bookService.GetBookCreateViewModelAsync();
        return View(formModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(BookFormModel model)
    {
        model.Genres = await genreService.GetAllGenresOrderedByNameAsync();
        
        if (!ModelState.IsValid)
            return View(model);

        bool bookExists = await bookService.IsBookExistByIsbnAsync(model.Isbn);
        if (bookExists)
        {
            ModelState.AddModelError(nameof(model.Isbn), "A Book with the same ISBN is already published!");
            return View(model);
        }
        
        bool genreExists = await genreService.IsExistByIdAsync(model.GenreId);
        if (!genreExists)
        {
            ModelState.AddModelError(nameof(model.GenreId), "Invalid Books's Genre is selected!");
            return View(model);
        }

        try
        {
            string publisherId = GetUserId()!;
            await bookService.AddBookAsync(model, publisherId);
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unexpected error occured while adding the Book in database!");
            ModelState.AddModelError(string.Empty, "Unexpected error occured while publishing the Book!");
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> FavouriteBooks()
    {
        string userId = GetUserId()!;
        
        IEnumerable<BookUserFavouriteBooksViewModel> favBooks = await bookService.GetUserFavoriteBooksAsync(userId);
        return View(favBooks);
    }

    [HttpPost]
    public async Task<IActionResult> AddToFavouriteBooks(int id, string? returnUrl)
    {
        returnUrl ??= Url.Action("Index")!;
        string userId = GetUserId()!;

        try
        {
            bool isUserPublisher = await bookService.IsBookPublisherAsync(id, userId);
            if (isUserPublisher)
            {
                TempData["ErrorMessage"] = "You cannot add your own book to your favourite collection!";
                return LocalRedirect(returnUrl);
            }

            bool isAlreadyAdded = await bookService.IsBookSavedAsync(id, userId);
            if (!isAlreadyAdded)
                await bookService.AddToMyFavoriteBooksAsync(id, userId);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unexpected error occured while Adding the Book in your Favourites!");
            TempData["ErrorMessage"] = "Unexpected error occured while Adding the Book in your Favourites! Please try again later.";
        }

        return LocalRedirect(returnUrl);
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromFavourite(int id)
    {
        string userId = GetUserId()!;

        try
        {
            bool isAlreadyAdded = await bookService.IsBookSavedAsync(id, userId);
            if (isAlreadyAdded)
                await bookService.RemoveFromMyFavouriteBooksAsync(id, userId);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unexpected error occured while Removing the Book from your Favourites!");
            TempData["ErrorMessage"] = "Unexpected error occured while Removing the Book from your Favourites! Please try again later.";
        }

        return RedirectToAction("FavouriteBooks");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        string userId = GetUserId()!;
        bool isUserPublisher = await bookService.IsBookPublisherAsync(id, userId);
        if (!isUserPublisher)
        {
            TempData["ErrorMessage"] = "You can edit only your own published books!";
            return RedirectToAction("Details", new { id });
        }
        
        BookEditInputModel? bookEditViewModel = await bookService.GetBookForEditAsync(id, userId);
        
        if (bookEditViewModel is null)
        {
            TempData["ErrorMessage"] = "The requested book does not exist!";
            return RedirectToAction("Index");
        }
        
        bookEditViewModel.Genres = await genreService.GetAllGenresOrderedByNameAsync(bookEditViewModel);
        
        return View(bookEditViewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(BookEditInputModel model)
    {
        model.Genres = await genreService.GetAllGenresOrderedByNameAsync();
        
        if (!ModelState.IsValid)
            return View(model);
        
        bool genreExists = await genreService.IsExistByIdAsync(model.GenreId);
        if (!genreExists)
        {
            ModelState.AddModelError(nameof(model.GenreId), "Invalid Books's Genre is selected!");
            return View(model);
        }

        try
        {
            string publisherId = GetUserId()!;
            await bookService.EditBookAsync(model, publisherId);
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unexpected error occured while editing the Book in database!");
            ModelState.AddModelError(string.Empty, "Unexpected error occured while editing the Book!");
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        BookDeleteViewModel? bookDeleteViewModel = await bookService.GetBookDeleteDetailsAsync(id);

        if (bookDeleteViewModel is null)
        {
            TempData["ErrorMessage"] = "The requested book does not exist!";
            return RedirectToAction("Index");
        }
        
        string userId = GetUserId()!;
        bool isUserPublisher = await bookService.IsBookPublisherAsync(id, userId);
        if (!isUserPublisher)
        {
            TempData["ErrorMessage"] = "You can delete only your own published books!";
            return RedirectToAction("Details", new { id });
        }
        
        return View(bookDeleteViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmDelete(int id)
    {
        bool isBookExists = await bookService.IsBookExistByIdAsync(id);
        if (!isBookExists)
        {
            TempData["ErrorMessage"] = "The requested book does not exist!";
            return RedirectToAction("Index");
        }
        
        string userId = GetUserId()!;
        bool isUserPublisher = await bookService.IsBookPublisherAsync(id, userId);
        if (!isUserPublisher)
        {
            TempData["ErrorMessage"] = "You can delete only your own published books!";
            return RedirectToAction("Details", new { id });
        }

        try
        {
            await bookService.DeleteBookAsync(id);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unexpected error occured while Deleting the Book!");
            TempData["ErrorMessage"] = "Unexpected error occured while Deleting the Book! Please try again later.";
        }
        
        return RedirectToAction("Index");
    }
}