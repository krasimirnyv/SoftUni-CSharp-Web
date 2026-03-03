namespace CinemaApp.Services.Core.Contracts;

using Web.ViewModels.Movie;

public interface IMovieService
{
    Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMovies();

    // Service to be refactored to work without coupling to ViewModels
    Task CreateMovieAsync(MovieFormModel formModel);
}