namespace Homies.ViewModels.Event;

public class EventDetailsViewModel
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Start { get; set; } = null!;

    public string End { get; set; } = null!;

    public bool IsUserOrganiser { get; set; }
    
    public string Organiser { get; set; } = null!;

    public string CreatedOn { get; set; } = null!;

    public string Type { get; set; } = null!;

    public IEnumerable<string> Participants { get; set; } 
        = new List<string>();
}

