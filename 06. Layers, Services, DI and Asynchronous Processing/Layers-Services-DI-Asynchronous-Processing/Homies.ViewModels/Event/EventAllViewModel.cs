namespace Homies.ViewModels.Event;

public class EventAllViewModel
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;
    
    public string Start { get; set; } = null!;

    public string? EventType { get; set; }

    public string? OrganiserName { get; set; }
    
    public bool CanJoin { get; set; }
}