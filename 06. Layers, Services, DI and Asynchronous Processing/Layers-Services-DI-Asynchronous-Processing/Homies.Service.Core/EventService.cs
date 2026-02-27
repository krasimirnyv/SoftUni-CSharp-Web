namespace Homies.Service.Core;

using System.Globalization;

using Interfaces;

using Data;
using Data.Models;

using ViewModels.Event;

using static GCommon.ApplicationConstants;

using Microsoft.EntityFrameworkCore;

/* Data layer offers DB-abstracted API for Data access -> DbContext */
public class EventService(HomiesDbContext context) : IEventService
{
    public async Task<IEnumerable<EventAllViewModel>> GetAllEventsOrderedAsync(string userId)
    {
        IQueryable<Event> fetchEventsQuery = context
            .Events
            .Include(e => e.Organiser)
            .Include(e => e.EventType)
            .Include(e => e.EventsParticipants)
            .AsNoTracking();
        
        IEnumerable<EventAllViewModel> allEventsViewModel = await fetchEventsQuery
            .OrderBy(e => e.Start)
            .ThenBy(e => e.Name)
            .ThenBy(e => e.EventType.Name)
            .ThenBy(e => e.Organiser.UserName)
            .Select(e => new EventAllViewModel
            {
                Id = e.Id.ToString(),
                Name = e.Name,
                Start = e.Start.ToString(DateTimeFormat, CultureInfo.InvariantCulture),
                EventType = e.EventType == null! ? null : e.EventType.Name,
                OrganiserName = e.Organiser == null! ? null : e.Organiser.UserName,
                CanJoin = e.EventsParticipants.All(ep => ep.HelperId.ToLower() != userId.ToLower())
            })
            .ToArrayAsync();

        return allEventsViewModel;
    }

    public async Task<EventAddInputModel> GetEmptyEventInputModelWithLoadedEventTypesAsync()
    {
        EventAddInputModel eventAddInputModel = new EventAddInputModel
        {
            EventTypes = await GetAllEventTypesForSelectEventAsync()
        };

        return eventAddInputModel;
    }
    
    public async Task<IEnumerable<SelectEventTypeViewModel>> GetAllEventTypesForSelectEventAsync()
    {
         IEnumerable<SelectEventTypeViewModel> allEventTypes = await context
            .EventTypes
            .AsNoTracking()
            .Select(et => new SelectEventTypeViewModel
            {
                Id = et.Id,
                Name = et.Name
            })
            .OrderBy(et => et.Name)
            .ToArrayAsync();

         return allEventTypes;
    }

    public async Task<bool> EventTypeExists(EventAddInputModel model)
    {
        model.EventTypes = await GetAllEventTypesForSelectEventAsync();

        bool eventTypeExists = model
            .EventTypes
            .Any(et => et.Id == model.EventTypeId);
        
        return eventTypeExists;
    }
    
    public async Task CreateEventAsync(EventAddInputModel model, string organiserId)
    {
        Event newEvent = new Event
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Description = model.Description,
            Start = model.Start,
            End = model.End,
            OrganiserId = organiserId,
            EventTypeId = model.EventTypeId
        };
            
        await context.Events.AddAsync(newEvent);
        await context.SaveChangesAsync();
    }

    public async Task<EventAddInputModel?> GetEventInputModelByIdAsync(Guid eventId)
    {
        Event? eventToEdit = await context
            .Events
            .Include(e => e.EventType)
            .SingleOrDefaultAsync(e => e.Id == eventId);

        if (eventToEdit is null)
            return null;
        
        EventAddInputModel eventEditInputModel = new EventAddInputModel
        {
            Name = eventToEdit.Name,
            Description = eventToEdit.Description,
            Start = eventToEdit.Start,
            End = eventToEdit.End,
            EventTypeId = eventToEdit.EventTypeId,
            EventTypes = await GetAllEventTypesForSelectEventAsync()
        };
        
        return eventEditInputModel;
    }
    
    public async Task<bool> EventExistsAsync(Guid eventId)
    {
        bool eventExists = await context
            .Events
            .AnyAsync(e => e.Id == eventId);
        
        return eventExists;
    }

    public async Task EditEventAsync(Guid eventId, EventAddInputModel model)
    {
        Event? editEvent = await context
            .Events
            .SingleOrDefaultAsync(e => e.Id == eventId);

        if (editEvent is null)
            throw new ArgumentException(ExceptionMessage);
        
        editEvent.Name = model.Name;
        editEvent.Description = model.Description;
        editEvent.Start = model.Start;
        editEvent.End = model.End;
        editEvent.EventTypeId = model.EventTypeId;

        context.Events.Update(editEvent);
        await context.SaveChangesAsync();
    }

    public async Task<EventDetailsViewModel?> GetEventDetailsByIdAsync(Guid eventId, string userId)
    {
        Event? eventDetails = await context
            .Events
            .Include(e => e.Organiser)
            .Include(e => e.EventType)
            .Include(e => e.Participants)
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Id == eventId);

        if (eventDetails is null)
            return null;
        
        EventDetailsViewModel eventDetailsViewModel = new EventDetailsViewModel
        {
            Id = eventDetails.Id.ToString(),
            Name = eventDetails.Name,
            Description = eventDetails.Description,
            Start = eventDetails.Start.ToString(DateTimeFormat, CultureInfo.InvariantCulture),
            End = eventDetails.End.ToString(DateTimeFormat, CultureInfo.InvariantCulture),
            IsUserOrganiser = eventDetails.OrganiserId.ToLower() == userId.ToLower(),
            Organiser = eventDetails.Organiser.UserName!,
            CreatedOn = eventDetails.CreatedOn.ToString(DateTimeFormat, CultureInfo.InvariantCulture),
            Type = eventDetails.EventType.Name,
            Participants = eventDetails.Participants
                .Select(p => p.UserName!)
                .OrderBy(un => un)
                .ToArray()
        };

        return eventDetailsViewModel;
    }
}