namespace CinemaApp.Data.Repository.Contracts;

using Models;

public interface IMovieRepository : IDisposable
{
    Task<IEnumerable<Movie>> GetAllMoviesNoTrackingWithProjectionAsync(Func<Movie, Movie>? projectFunction = null);
    
    Task<IEnumerable<Movie>> GetAllMoviesAsync();
    
    Task<Movie?> GetMovieByIdAsync(Guid movieId);
    
    Task<bool> AddMovieAsync(Movie movie);
    
    Task<bool> UpdateMovieAsync(Movie movie);

    Task<bool> SoftDeleteMovieAsync(Movie movie);

    Task<bool> HardDeleteMovieAsync(Movie movie);

    Task<bool> ExistsByIdAsync(Guid movieId);
}