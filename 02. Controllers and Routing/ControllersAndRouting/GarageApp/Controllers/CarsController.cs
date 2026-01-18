namespace GarageApp.Controllers;

using Data;
using Models;
using Models.Enums;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class CarsController : Controller
{
    private readonly GarageDbContext dbContext;
    
    public CarsController(GarageDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        IEnumerable<Car> allCars = dbContext
            .Cars
            .Include(c => c.Garage)
            .AsSplitQuery()
            .AsNoTracking()
            .OrderBy(c => c.Make)
            .ThenBy(c => c.Model)
            .ThenByDescending(c => c.Year)
            .ThenBy(c => c.BodyStyle)
            .ThenBy(c => c.IsAvailable)
            .ToArray();

        return View(allCars);
    }
    
    [HttpGet]
    public IActionResult Details(string? id)
    {
        if (String.IsNullOrEmpty(id))
        {
            return View("BadRequest", "Invalid car identifier.");
        }
        
        bool isGuidValid = Guid.TryParse(id, out Guid guidId);
        if (!isGuidValid)
        {
            return View("BadRequest", "Invalid car identifier.");
        }
        
        Car? car = dbContext
            .Cars
            .Include(c => c.Garage)
            .AsSplitQuery()
            .AsNoTracking()
            .SingleOrDefault(c => c.Id == guidId);

        if (car == null)
        {
            return View("NotFound", "Car doesn't exist!");
        }
        
        return View(car);
    }
    
    [HttpGet]
    public IActionResult Search(string? searchString)
    {
        IQueryable<Car> carsQuery = dbContext
            .Cars
            .Include(c => c.Garage)
            .AsSplitQuery()
            .AsNoTracking();
        
        if (!String.IsNullOrWhiteSpace(searchString))
        {
            string pattern = $"%{searchString}%";
            
            if (int.TryParse(searchString, out int year))
            {
                carsQuery = carsQuery.Where(c => c.Year == year);
            }
            else if (Enum.TryParse<CarBodyStyle>(searchString, true, out var bodyStyle))
            {
                carsQuery = carsQuery.Where(c => c.BodyStyle == bodyStyle);
            }
            else
            {
                carsQuery = carsQuery.Where(c =>
                    EF.Functions.Like(c.Make, pattern) ||
                    EF.Functions.Like(c.Model, pattern));
            }
        }

        IEnumerable<Car> cars = carsQuery
            .OrderBy(c => c.Make)
            .ThenBy(c => c.Model)
            .ThenByDescending(c => c.Year)
            .ThenBy(c => c.BodyStyle)
            .ThenBy(c => c.IsAvailable)
            .ToArray();
        
        return View("Index", cars);
    }
}