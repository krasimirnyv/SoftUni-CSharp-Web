namespace EventManager.Controllers;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using Data;
using Models;
using ViewModels.Event;
using ViewModels.Registration;

public class EventsController : Controller
{
    private readonly EventDbContext dbContext;
    
    public EventsController(EventDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        IEnumerable<AllEventsViewModel> events = dbContext
            .Events
            .Include(e => e.Category)
            .AsNoTracking()
            .OrderBy(e => e.Title)
            .Select(e => new AllEventsViewModel()
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                StartDate = e.StartDate.ToString("dd MMM yyyy, HH:mm"),
                EndDate = e.EndDate.ToString("dd MMM yyyy, HH:mm"),
                MaxParticipants = e.MaxParticipants,
                CategoryName = e.Category.Name
            })
            .ToArray();
        
        return View(events);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        IEnumerable<Category> allCategories = FetchCategories();

        CreateEventInputModel inputModel = new CreateEventInputModel()
        {
            Categories = allCategories
        };
        
        return View(inputModel);
    }

    [HttpPost]
    public IActionResult Create(CreateEventInputModel inputModel)
    {
        IEnumerable<Category> allCategories = FetchCategories();
        inputModel.Categories = allCategories;

        if (!ModelState.IsValid)
        {
            return View(inputModel);
        }

        bool isSelectedCategoryValid = allCategories
            .Any(c => c.Id == inputModel.CategoryId);
        if (!isSelectedCategoryValid)
        {
            ModelState.AddModelError(nameof(inputModel.CategoryId), "Invalid category is selected.");
            return View(inputModel);
        }
        
        try
        {
            Event newEvent = new Event()
            {
                Title = inputModel.Title,
                Description = inputModel.Description,
                StartDate = inputModel.StartDate,
                EndDate = inputModel.EndDate,
                MaxParticipants = inputModel.MaxParticipants,
                CategoryId = inputModel.CategoryId
            };
            
            dbContext.Events.Add(newEvent);
            dbContext.SaveChanges();
        }
        catch (Exception e)
        {
            // DB validation fails
            Console.WriteLine(e);

            return View("Error");
        }
        
        TempData["SuccessMessage"] = "Event created successfully";
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public IActionResult Details(int id)
    {
        Event? eventEntity = dbContext
            .Events
            .Include(e => e.Category)
            .Include(e => e.Registrations)
            .AsNoTracking()
            .FirstOrDefault(e => e.Id == id);

        if (eventEntity == null)
        {
            return NotFound();
        }

        DetailsEventViewModel viewModel = new DetailsEventViewModel
        {
            Id = eventEntity.Id,
            Title = eventEntity.Title,
            Description = eventEntity.Description,
            StartDate = eventEntity.StartDate.ToString("dd MMM yyyy, HH:mm"),
            EndDate = eventEntity.EndDate.ToString("dd MMM yyyy, HH:mm"),
            MaxParticipants = eventEntity.MaxParticipants,
            CategoryName = eventEntity.Category.Name,
            Registrations = eventEntity.Registrations
                .Select(r => new RegisteredParticipantsViewModel
                {
                    ParticipantName = r.ParticipantName,
                    Email = r.Email
                })
                .OrderBy(r => r.ParticipantName)
                .ToArray()
        };
        
        return View(viewModel);
    }
    
    private IEnumerable<Category> FetchCategories()
    {
        return dbContext
            .Categories
            .AsNoTracking()
            .OrderBy(e => e.Name)
            .ToArray();
    }
}