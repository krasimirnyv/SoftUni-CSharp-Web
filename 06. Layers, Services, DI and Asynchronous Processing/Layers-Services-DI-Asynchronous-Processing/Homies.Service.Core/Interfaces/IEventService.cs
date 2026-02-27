namespace Homies.Service.Core.Interfaces;

using ViewModels.Event;

public interface IEventService
{
    Task<IEnumerable<EventAllViewModel>> GetAllEventsOrderedAsync(string userId);

    Task<EventAddInputModel> GetEmptyEventInputModelWithLoadedEventTypesAsync();

    Task<IEnumerable<SelectEventTypeViewModel>> GetAllEventTypesForSelectEventAsync();

    Task<bool> EventTypeExists(EventAddInputModel model);
        
    Task CreateEventAsync(EventAddInputModel model, string organiserId);
    
    Task<EventAddInputModel?> GetEventInputModelByIdAsync(Guid eventId);
    
    Task<bool> EventExistsAsync(Guid eventId);
    
    Task EditEventAsync(Guid eventId, EventAddInputModel model);

    Task<EventDetailsViewModel?> GetEventDetailsByIdAsync(Guid eventId, string userId);
}