namespace BookShelf.Controllers;

using Models;
using Data;

using static Common.Messages;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

public class AuthorsController : Controller
{
    private readonly ApplicationDbContext dbContext;
    
    public AuthorsController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        IEnumerable<Author> authors = dbContext
            .Authors
            .AsNoTracking()
            .OrderBy(a => a.Name)
            .ThenBy(a => a.Country)
            .ThenBy(a => a.Books.Count)
            .ToArray();
        
        return View(authors);
    }
    
    [HttpGet]
    public IActionResult Details(string? id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return View("BadRequest", string.Format(InvalidIdentifier, nameof(Author)));
        }
        
        bool isGuidValid = Guid.TryParse(id, out Guid authorId);
        if (!isGuidValid)
        {
            return View("BadRequest", string.Format(InvalidIdentifier, nameof(Author)));
        }
        
        Author? author = dbContext
            .Authors
            .Include(a => a.Books
                .OrderBy(b => b.Title))
            .AsNoTracking()
            .AsSplitQuery()
            .SingleOrDefault(a => a.Id == authorId);
        
        if (author == null)
        {
            return View("NotFound", string.Format(NotFoundMessage, nameof(Author)));
        }
        
        return View(author);
    }
}