namespace Homies.Service.Core.Interfaces;

using ViewModels.Event;

public interface IEventUserService
{
    Task<bool> IsUserOrganiserOfEventAsync(Guid eventId, string userId);

    Task<IEnumerable<EventAllViewModel>> GetJoinedEventsByUserIdAsync(string userId);
    
    Task<bool> JoinEventAsync(Guid eventId, string userId);
    
    Task<bool> LeaveEventAsync(Guid eventId, string userId);
}