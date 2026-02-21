namespace GameZone.ViewModels;

using System.ComponentModel.DataAnnotations;

using static Common.ValidationConstants;

public class GameAddInputModel
{
    // InputModel behavior (View -> Controller)
    [Required]
    [StringLength(GameTitleMaxLength, MinimumLength = GameTitleMinLength)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(GameDescriptionMaxLength, MinimumLength = GameDescriptionMinLength)]
    public string Description { get; set; } = null!;

    [Url]
    [StringLength(GameImageUrlMaxLength, MinimumLength = GameImageUrlMinLength)]
    public string? ImageUrl { get; set; }

    [Required]
    [StringLength(GamePublisherNameMaxLength, MinimumLength = GamePublisherNameMinLength)]
    public string PublisherName { get; set; } = null!;

    [Required]
    public DateOnly ReleasedOn { get; set; }

    [Required]
    public int GenreId { get; set; }

    // ViewModel behavior (Controller -> View)
    // Partial output data for populating the genre dropdown in the view.
    // Data required for the user input.
    public IEnumerable<GenreViewModel> Genres { get; set; }
        = new List<GenreViewModel>();
}