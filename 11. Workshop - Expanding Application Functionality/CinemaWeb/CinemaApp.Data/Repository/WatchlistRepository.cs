namespace CinemaApp.Data.Repository;

using Models;
using Contracts;

using Microsoft.EntityFrameworkCore;

public class WatchlistRepository : BaseRepository, IWatchlistRepository
{
    public WatchlistRepository(CinemaDbContext dbContext) 
        : base(dbContext)
    {
    }

    public async Task<IEnumerable<UserMovie>> GetAllUserMoviesAsync()
    {
        IEnumerable<UserMovie> userMovies = await Context
            .UsersMovies
            .Include(um => um.Movie)
            .AsNoTracking()
            .ToArrayAsync();
        
        return userMovies;
    }

    public async Task<UserMovie?> GetUserMovieAsync(string userId, Guid movieId)
    {
        UserMovie? userMovie = await Context
            .UsersMovies
            .SingleOrDefaultAsync(um => um.UserId.ToLower() == userId.ToLower() &&
                                        um.MovieId == movieId);
        
        return userMovie;
    }

    public async Task<UserMovie?> GetUserMovieIncludeDeletedAsync(string userId, Guid movieId)
    {
        UserMovie? userMovie = await Context
            .UsersMovies
            .IgnoreQueryFilters()
            .SingleOrDefaultAsync(um => um.UserId.ToLower() == userId.ToLower() &&
                                        um.MovieId == movieId);
        
        return userMovie;
    }

    public async Task<bool> ExistsAsync(string userId, Guid movieId)
    {
        bool watchlistEntryExist = await Context
            .UsersMovies
            .AnyAsync(um => um.UserId.ToLower() == userId.ToLower() && um.MovieId == movieId);

        return watchlistEntryExist;
    }

    public async Task<bool> AddUserMovieAsync(UserMovie userMovie)
    {
        await Context.UsersMovies.AddAsync(userMovie);
        int resultCount = await SaveChangesAsync();

        return resultCount == 1;
    }

    public async Task<bool> UpdateUserMovieAsync(UserMovie userMovie)
    {
        Context.UsersMovies.Update(userMovie);
        int resultCount = await SaveChangesAsync();

        return resultCount == 1;
    }

    public async Task<bool> SoftDeleteUserMovieAsync(UserMovie userMovie)
    {
        userMovie.IsDeleted = true;

        Context.UsersMovies.Update(userMovie);
        int resultCount = await SaveChangesAsync();
        
        return resultCount == 1;
    }
}