namespace Homies.Service.Core;

using System.Globalization;

using Interfaces;

using Data;
using Data.Models;

using ViewModels.Event;

using static GCommon.ApplicationConstants;

using Microsoft.EntityFrameworkCore;

public class EventUserService(HomiesDbContext context): IEventUserService
{
    public async Task<bool> IsUserOrganiserOfEventAsync(Guid eventId, string userId)
    {
        Event? checkEvent = await context
            .Events
            .Include(e => e.EventType)
            .SingleOrDefaultAsync(e => e.Id == eventId);

        if (checkEvent is null)
            throw new ArgumentException(ExceptionMessage);

        return checkEvent.OrganiserId.ToLower() == userId.ToLower();
    }
    
    public async Task<IEnumerable<EventAllViewModel>> GetJoinedEventsByUserIdAsync(string userId)
    {
        IEnumerable<EventAllViewModel> joinedEvents = await context
            .EventsParticipants
            .Include(ep => ep.Event)
            .Include(ep => ep.Helper)
            .AsNoTracking()
            .Where(ep => ep.HelperId.ToLower() == userId.ToLower())
            .Select(ep => ep.Event)
            .OrderBy(e => e.Start)
            .ThenBy(e => e.Name)
            .ThenBy(e => e.EventType.Name)
            .ThenBy(e => e.Organiser.UserName)
            .Select(e => new EventAllViewModel
            {
                Id = e.Id.ToString(),
                Name = e.Name,
                Start = e.Start.ToString(DateTimeFormat, CultureInfo.InvariantCulture),
                EventType = e.EventType.Name,
                OrganiserName = e.Organiser.UserName
            })
            .ToArrayAsync();

        return joinedEvents;
    }

    public async Task<bool> JoinEventAsync(Guid eventId, string userId)
    {
        Event? eventToJoin = await context
            .Events
            .Include(e => e.Participants)
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Id == eventId);

        if (eventToJoin is null)
            return false;

        bool isOrganiser = string.Equals(eventToJoin
            .OrganiserId, userId, StringComparison.InvariantCultureIgnoreCase);

        if (isOrganiser)
            return false;

        bool isAlreadyJoined = eventToJoin
            .Participants
            .Any(p => string.Equals(p.Id, userId, StringComparison.InvariantCultureIgnoreCase));

        if (!isAlreadyJoined)
        {
            EventParticipant newEventParticipant = new EventParticipant
            {
                EventId = eventToJoin.Id,
                HelperId = userId
            };

            await context.EventsParticipants.AddAsync(newEventParticipant);
            await context.SaveChangesAsync();
        }

        return true;
    }

    public async Task<bool> LeaveEventAsync(Guid eventId, string userId)
    {
        Event? eventToLeave = await context
            .Events
            .Include(e => e.Participants)
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Id == eventId);

        if (eventToLeave is null)
            return false;
        
        bool isOrganiser = string.Equals(eventToLeave
            .OrganiserId, userId, StringComparison.InvariantCultureIgnoreCase);
        
        if (isOrganiser)
            return false;
        
        EventParticipant? eventParticipant = await context
            .EventsParticipants
            .SingleOrDefaultAsync(ep => (ep.HelperId.ToLower() == userId.ToLower()) && (ep.EventId == eventId));

        if (eventParticipant is not null)
        {
            context.EventsParticipants.Remove(eventParticipant);
            await context.SaveChangesAsync();
        }

        return true;
    }
}