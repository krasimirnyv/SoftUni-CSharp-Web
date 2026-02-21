namespace GameZone.ViewModels;

public class GameViewModel
{
    public string Id { get; set; } = null!;
    
    public string Title { get; set; } = null!;
    
    public string? ImageUrl { get; set; }
    
    public string PublisherName { get; set; } = null!;

    public string ReleasedOn { get; set; } = null!;
    
    public string Genre { get; set; } = null!;
}