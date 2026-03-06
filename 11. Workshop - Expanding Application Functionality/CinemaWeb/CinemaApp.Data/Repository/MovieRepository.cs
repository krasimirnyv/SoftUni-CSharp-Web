namespace CinemaApp.Data.Repository;

using Contracts;
using Models;

using Microsoft.EntityFrameworkCore;

public class MovieRepository : BaseRepository, IMovieRepository
{
    public MovieRepository(CinemaDbContext dbContext)
        : base(dbContext)
    {
    }

    public IQueryable<Movie> GetAllMoviesNoTracking()
    {
        return Context
            .Movies
            .AsNoTracking();
    }

    public async Task<IEnumerable<Movie>> GetAllMoviesNoTrackingWithProjectionAsync(
        Func<Movie, Movie>? projectFunction = null)
    {
        IQueryable<Movie> moviesFetchQuery = Context
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
        return await Context
            .Movies
            .AsNoTracking()
            .OrderBy(m => m.Title)
            .ToArrayAsync();
    }

    public async Task<Movie?> GetMovieByIdAsync(Guid movieId)
    {
        return await Context
            .Movies
            .FindAsync(movieId);
    }

    public async Task<bool> AddMovieAsync(Movie movie)
    {
        await Context.Movies.AddAsync(movie);
        int resultCount = await SaveChangesAsync();

        return resultCount == 1;
    }

    public async Task<bool> UpdateMovieAsync(Movie movie)
    {
        Context.Movies.Update(movie);
        int resultCount = await SaveChangesAsync();

        return resultCount == 1;
    }

    public async Task<bool> SoftDeleteMovieAsync(Movie movie)
    {
        movie.IsDeleted = true;
        Context.Movies.Update(movie);

        // Demo variant: 
        // foreach (UserMovie watchlistEntry in movie.MovieUsersWatchlist)
        // {
        //     watchlistEntry.IsDeleted = true;
        // }

        int resultCount = await SaveChangesAsync();

        return resultCount == 1;
    }

    public async Task<bool> HardDeleteMovieAsync(Movie movie)
    {
        Context.Movies.Remove(movie);
        int resultCount = await SaveChangesAsync();

        return resultCount == 1;
    }

    public async Task<bool> ExistsByIdAsync(Guid movieId)
    {
        return await Context
            .Movies
            .AnyAsync(m => m.Id == movieId);
    }
}