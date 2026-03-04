using CinemaApp.Services.AutoMapping;

namespace CinemaApp.Services.Models.Movie;

public class MovieDetailsDto : MovieAllDto
{
    public int Duration { get; set; }

    public string Description { get; set; } = null!;
}