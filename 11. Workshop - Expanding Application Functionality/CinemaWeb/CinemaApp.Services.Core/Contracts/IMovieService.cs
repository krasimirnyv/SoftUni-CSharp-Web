namespace CinemaApp.Services.Core.Contracts;

using Web.ViewModels.Movie;

using Models.Movie;


public interface IMovieService
{
    Task<IEnumerable<MovieAllDto>> GetAllMoviesOrderedAsync(string? userId = null);

    Task CreateMovieAsync(MovieDetailsDto movieDetailsDto);

    Task<MovieDetailsDto?> GetMovieDetailsByIdAsync(Guid movieId);
    
    Task<MovieDetailsDto?> GetMovieFormModelByIdAsync(Guid movieId);
    
    Task<bool> ExistsByIdAsync(Guid movieId);
    
    Task EditMovieAsync(Guid movieId, MovieDetailsDto movieDetailsDto);
    
    Task SoftDeleteMovieAsync(Guid movieId);
    
    Task HardDeleteMovieAsync(Guid movieId);
}