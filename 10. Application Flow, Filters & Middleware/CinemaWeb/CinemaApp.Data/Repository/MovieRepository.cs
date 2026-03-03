namespace CinemaApp.Data.Repository;

using Contracts;
using Models;

using Microsoft.EntityFrameworkCore;

public class MovieRepository(CinemaDbContext context) : IMovieRepository
{
    public IQueryable<Movie> GetAllMoviesNoTracking()
    {
        return context
            .Movies
            .AsNoTracking();
    }

    public async Task<IEnumerable<Movie>> GetAllMovies()
    {
        return await context
            .Movies
            .AsNoTracking()
            .OrderBy(m => m.Title)
            .ToArrayAsync();
    }

    public async Task<bool> AddMovieAsync(Movie movie)
    {
        await context.Movies.AddAsync(movie);
        int resultCount = await SaveChangesAsync();
        
        return resultCount == 1;
    }

    private async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}