namespace CinemaApp.Data.Models;

using System.ComponentModel.DataAnnotations;

using static GCommon.EntityValidation.Movie;

public class Movie
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required, MaxLength(TitleMaxLenght)]
    public string Title { get; set; } = null!;

    [Required, MaxLength(GenreMaxLenght)]
    public string Genre { get; set; } = null!;

    public DateOnly ReleaseDate { get; set; }

    [Required, MaxLength(DirectorMaxLenght)]
    public string Director { get; set; } = null!;

    public int Duration { get; set; }

    [Required, MaxLength(DescriptionMaxLenght)]
    public string Description { get; set; } = null!;

    [MaxLength(ImageUrlMaxLenght)]
    public string? ImageUrl { get; set; }

    public bool IsDeleted { get; set; } = false;
    
}
