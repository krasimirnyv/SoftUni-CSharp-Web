using Microsoft.EntityFrameworkCore;

namespace EventManager.Controllers;

using Data;
using Models;

using Microsoft.AspNetCore.Mvc;

// The documentation for this controller is wrong by the provider, because it states
// about books and authors instead of events and registrations.
// Also, it is not provided a view or something to continue the implementation of the registration process.
public class RegistrationsController : Controller
{
    private readonly EventDbContext dbContext;
    
    public RegistrationsController(EventDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public IActionResult Create([FromQuery]int eventId)
    {
        Event? eventEntity = dbContext
            .Events
            .AsNoTracking()
            .SingleOrDefault(e => e.Id == eventId);
        
        if (eventEntity == null)
        {
            return NotFound();
        }
        
        bool isRegistrationOpen = dbContext
            .Registrations
            .AsNoTracking()
            .Count(r => r.EventId == eventId) < eventEntity.MaxParticipants;
        
        if (!isRegistrationOpen)
        {
            ModelState.AddModelError(string.Empty, "Registration could not be opened.");
            return RedirectToAction("Index", "Events");
        }
        
        return View();
    }
}