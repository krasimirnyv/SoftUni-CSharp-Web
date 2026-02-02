namespace EventManager.ViewModels.Event;

using Registration;

public class DetailsEventViewModel
{
    public int Id { get; set; }
    
    public string Title { get; set; } = null!;
    
    public string? Description  { get; set; }
    
    public string StartDate { get; set; } = null!;
    
    public string EndDate { get; set; } = null!;
    
    public int MaxParticipants { get; set; }

    public string? CategoryName { get; set; }
    
    public ICollection<RegisteredParticipantsViewModel>? Registrations { get; set; }
}