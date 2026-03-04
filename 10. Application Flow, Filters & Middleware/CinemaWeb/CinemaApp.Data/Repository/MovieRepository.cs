namespace CinemaApp.Data.Repository;

using Contracts;
using Models;

using Microsoft.EntityFrameworkCore;

public class MovieRepository(CinemaDbContext context) : IMovieRepository
{
    private bool isDisposed = false;
    
    public IQueryable<Movie> GetAllMoviesNoTracking()
    {
        return context
            .Movies
            .AsNoTracking();
    }

    public async Task<IEnumerable<Movie>> GetAllMoviesNoTrackingWithProjectionAsync(Func<Movie, Movie>? projectFunction = null)
    {
        IQueryable<Movie> moviesFetchQuery = context
            .Movies
            .AsNoTracking()
            .OrderBy(m => m.Title);

        if (projectFunction is not null)
        {
            moviesFetchQuery = moviesFetchQuery
                .Select(m => projectFunction(m))
                .AsQueryable();
        }

        return await moviesFetchQuery.ToArrayAsync();
    }

    public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
    {
        return await context
            .Movies
            .AsNoTracking()
            .OrderBy(m => m.Title)
            .ToArrayAsync();
    }

    public async Task<Movie?> GetMovieByIdAsync(Guid movieId)
    {
        return await context
            .Movies
            .FindAsync(movieId);
    }

    public async Task<bool> AddMovieAsync(Movie movie)
    {
        await context.Movies.AddAsync(movie);
        int resultCount = await SaveChangesAsync();
        
        return resultCount == 1;
    }

    public async Task<bool> UpdateMovieAsync(Movie movie)
    { 
        context.Movies.Update(movie);
        int resultCount = await SaveChangesAsync();
        
        return resultCount == 1;
    }

    public async Task<bool> SoftDeleteMovieAsync(Movie movie)
    {
        movie.IsDeleted = true;

        context.Movies.Update(movie);
        int resultCount = await SaveChangesAsync();

        return resultCount == 1;
    }

    public async Task<bool> HardDeleteMovieAsync(Movie movie)
    { 
        context.Movies.Remove(movie);
        int resultCount = await SaveChangesAsync();
        
        return resultCount == 1;
    }

    public async Task<bool> ExistsByIdAsync(Guid movieId)
    {
        return await context
            .Movies
            .AnyAsync(m => m.Id == movieId);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        
        isDisposed = true;
    }
    
    private async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}