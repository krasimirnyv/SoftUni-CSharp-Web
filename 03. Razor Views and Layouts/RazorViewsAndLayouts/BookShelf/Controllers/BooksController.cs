namespace BookShelf.Controllers;

using Models;
using Data;

using static Common.Messages;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

public class BooksController : Controller
{
    private readonly ApplicationDbContext dbContext;
    
    public BooksController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        IEnumerable<Book> books = dbContext
            .Books
            .Include(b => b.Author)
            .AsNoTracking()
            .AsSplitQuery()
            .OrderBy(b => b.Title)
            .ThenBy(b => b.Author.Name)
            .ThenByDescending(b => b.Year)
            .ToArray();
        
        return View(books);
    }

    [HttpGet]
    public IActionResult Create()
    {
        IEnumerable<Author> authors = dbContext
            .Authors
            .AsNoTracking()
            .OrderBy(a => a.Name)
            .ThenBy(a => a.Country)
            .ThenBy(a => a.Books.Count)
            .ToArray();
        
        // It's always better to use ViewModels for passing data to Views, instead of unsafe ViewData/ViewBag.
        // This is demo that we can use ViewData/ViewBag for data transfer.
        // I. Controller <-> View
        // II. View <-> View
        
        ViewData["Authors"] = authors;
        
        return View();
    }

    [HttpPost]
    public IActionResult Create(Book inputModel)
    {
        try
        {
            Author? refAuthor = dbContext
                .Authors
                .Find(inputModel.AuthorId);
            
            if (refAuthor == null)
            {
                ModelState.AddModelError(nameof(Book), string.Format(NoAuthorReference, nameof(Author), nameof(Book)));
                return View();
            }
            
            dbContext.Books.Add(inputModel);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        catch (Exception exception)
        {
            return View("BadRequest", exception.Message);
        }
    }
}