namespace CinemaApp.Web.ViewModels.Movie;

using Services.AutoMapping;

using CinemaApp.Services.Models.Movie;

public class MovieDetailsViewModel : AllMoviesIndexViewModel, IMapFrom<MovieDetailsDto>
{
    public int Duration { get; set; }

    public string Description { get; set; } = null!;
}