namespace GarageApp.Controllers;

using Data;
using Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class GarageController : Controller
{
    private readonly GarageDbContext dbContext;

    public GarageController(GarageDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        IEnumerable<Garage> allGarages = dbContext
            .Garages
            .Include(g => g.Cars)
            .AsSplitQuery()
            .AsNoTracking()
            .OrderBy(g => g.Name)
            .ToArray();
        
        return View(allGarages);
    }

    [HttpGet]
    public IActionResult Details(string? id)
    {
        if (String.IsNullOrEmpty(id))
        {
            return View("BadRequest", "Invalid garage identifier.");
        }
        
        bool isGuidValid = Guid.TryParse(id, out Guid guidId);
        if (!isGuidValid)
        {
            return View("BadRequest", "Invalid garage identifier.");
        }
        
        Garage? garage = dbContext
            .Garages
            .Include(g => g.Cars)
            .AsSplitQuery()
            .AsNoTracking()
            .SingleOrDefault(g => g.Id == guidId);

        if (garage == null)
        {
            return View("NotFound", "Garage doesn't exist!");
        }
        
        return View(garage);
    }
}