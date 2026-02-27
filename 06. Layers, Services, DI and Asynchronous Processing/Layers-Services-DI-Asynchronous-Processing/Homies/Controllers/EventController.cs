namespace Homies.Controllers;

using System.Security.Claims;

using ViewModels.Event;

using Service.Core.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class EventController(IEventService eventService,
                             IEventUserService userService,
                             ILogger<EventController> logger) : Controller
{
    [HttpGet]
    public async Task<IActionResult> All()
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        /* The async method of the Service may need to be AWAITED -> Action also need to be async */
        IEnumerable<EventAllViewModel> allEventsViewModels = await eventService.GetAllEventsOrderedAsync(userId);
        
        return View(allEventsViewModels);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        EventAddInputModel eventAddInputModel = await eventService.GetEmptyEventInputModelWithLoadedEventTypesAsync();
        
        return View(eventAddInputModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(EventAddInputModel? model)
    {
        if (model is null)
            return BadRequest();

        bool eventTypeExists = await eventService.EventTypeExists(model);
        if (!eventTypeExists)
            ModelState.AddModelError(nameof(model.EventTypeId), "Invalid event type is selected!");
        
        if (!ModelState.IsValid)
            return View(model);

        string organiserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        try
        {
            await eventService.CreateEventAsync(model, organiserId);
            return RedirectToAction("All");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Exception occured while trying to create and save an Event in database!");
            ModelState.AddModelError(string.Empty, "An error occured while saving the event. Please try again later.");
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string? id)
    {
        bool isGuidValid = Guid.TryParse(id, out Guid eventId);
        if (!isGuidValid)
            return BadRequest();

        EventAddInputModel? eventToEdit = await eventService.GetEventInputModelByIdAsync(eventId);

        if (eventToEdit is null)
            return NotFound();
        
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        try
        {
            bool isUserOrganiser = await userService.IsUserOrganiserOfEventAsync(eventId, userId);
            
            if (!isUserOrganiser)
                return Unauthorized();
        }
        catch (ArgumentException e)
        {
            logger.LogError(e, "Exception occured while trying to find Event with Organiser! The checkEvent in the IsUserOrganiserOfEventAsync was null");
            return NotFound();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Exception occured while trying to find Event with Organiser!");
            return BadRequest();
        }
        
        return View(eventToEdit);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromRoute] string? id, EventAddInputModel? model)
    {
        bool isGuidValid = Guid.TryParse(id, out Guid eventId);
        if (model is null || !isGuidValid)
            return BadRequest();
        
        bool eventExists = await eventService.EventExistsAsync(eventId);
        if (!eventExists)
            return NotFound();
        
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        try
        {
            bool isUserOrganiser = await userService.IsUserOrganiserOfEventAsync(eventId, userId);

            if (!isUserOrganiser)
                return Unauthorized();
        }
        catch (ArgumentException e)
        {
            logger.LogError(e, "Exception occured while trying to find Event with Organiser! The checkEvent in the IsUserOrganiserOfEventAsync was null");
            return NotFound();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Exception occured while trying to find Event with Organiser!");
            return BadRequest();
        }
        
        bool eventTypeExists = await eventService.EventTypeExists(model);
        if (!eventTypeExists)
            ModelState.AddModelError(nameof(model.EventTypeId), "Invalid event type is selected!");

        if (!ModelState.IsValid)
            return View(model);
        
        try
        {
            await eventService.EditEventAsync(eventId, model);
            
            return RedirectToAction("All");
        }
        catch (ArgumentException e)
        {
            logger.LogError(e, "Exception occured while trying to update and save an Event in database! The Event's ID is not found!");
            ModelState.AddModelError(string.Empty, $"Event with ID {eventId} is not found!");
            return View(model);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Exception occured while trying to update and save an Event in database!");
            ModelState.AddModelError(string.Empty, "An error occured while updating the event. Please try again later.");
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(string? id)
    {
        bool isGuidValid = Guid.TryParse(id, out Guid eventId);
        if (!isGuidValid)
            return BadRequest();

        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        EventDetailsViewModel? eventDetailsViewModel = await eventService.GetEventDetailsByIdAsync(eventId, userId);

        if (eventDetailsViewModel is null)
            return NotFound();
        
        return View(eventDetailsViewModel);
    }
    
    [HttpGet]
    public async Task<IActionResult> Joined()
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        IEnumerable<EventAllViewModel> joinedEvents = await userService.GetJoinedEventsByUserIdAsync(userId);

        return View(joinedEvents);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Join(string? id)
    {
        bool isGuidValid = Guid.TryParse(id, out Guid eventId);
        if (!isGuidValid)
            return BadRequest();

        bool eventExists = await eventService.EventExistsAsync(eventId);
        if (!eventExists)
            return NotFound();
        
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        try
        {
            bool joinedSuccessful = await userService.JoinEventAsync(eventId, userId);
            if (!joinedSuccessful)
                return BadRequest();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Exception occured while trying to Join an Event!");
            return BadRequest();
        }
        
        return RedirectToAction("Joined");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Leave(string? id)
    {
        bool isGuidValid = Guid.TryParse(id, out Guid eventId);
        if (!isGuidValid)
            return BadRequest();

        bool eventExists = await eventService.EventExistsAsync(eventId);
        if (!eventExists)
            return NotFound();
        
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        try
        {
            bool leavedSuccessful = await userService.LeaveEventAsync(eventId, userId);
            if (!leavedSuccessful)
                return BadRequest();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Exception occured while trying to Leave an Event!");
            return BadRequest();
        }
        
        return RedirectToAction("Joined");
    }
}