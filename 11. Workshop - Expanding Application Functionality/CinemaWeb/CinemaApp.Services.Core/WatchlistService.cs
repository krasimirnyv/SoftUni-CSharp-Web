namespace CinemaApp.Services.Core;

using Contracts;
using Models.Watchlist;

using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;

using AutoMapper;

using GCommon.Exceptions;

public class WatchlistService(IWatchlistRepository watchlistRepository, IMovieRepository movieRepository, IMapper mapper) : IWatchlistService
{
    public async Task<IEnumerable<WatchlistMovieDto>> GetUserWatchlistByIdAsync(string userId)
    {
        IEnumerable<Movie> userWatchlist = watchlistRepository
            .GetAllUserMoviesAsync()
            .GetAwaiter()
            .GetResult()
            .Where(um => um.UserId.ToLower() == userId.ToLower())
            .Select(um => um.Movie)
            .ToArray();

        IEnumerable<WatchlistMovieDto> watchlistMovieDtos = mapper.Map<IEnumerable<WatchlistMovieDto>>(userWatchlist);

        return watchlistMovieDtos;
    }

    public async Task AddMovieToUserWatchlistAsync(string userId, Guid movieId)
    {
        UserMovie? userMovie = await watchlistRepository.GetUserMovieIncludeDeletedAsync(userId, movieId);
        if (userMovie is not null && userMovie.IsDeleted == false)
            throw new EntityAlreadyExistsException();

        bool movieExists = await movieRepository.ExistsByIdAsync(movieId);
        if (!movieExists)
            throw new EntityNotFoundException();

        bool successPersist = false;
        if (userMovie is null)
        {
            UserMovie newUserMovie = new UserMovie
            {
                UserId = userId,
                MovieId = movieId
            };
            
            successPersist = await watchlistRepository.AddUserMovieAsync(newUserMovie);
        }
        else
        {
            // Recover soft-delete entry
            userMovie.IsDeleted = false;
            
            successPersist = await watchlistRepository.UpdateUserMovieAsync(userMovie);
        }
        
        if (!successPersist)
            throw new EntityPersistFailureException();
    }

    public async Task RemoveMovieFromUserWatchlistAsync(string userId, Guid movieId)
    {
        UserMovie? userMovie = await watchlistRepository.GetUserMovieAsync(userId, movieId);
        if (userMovie is null)
            throw new EntityNotFoundException();
        
        bool successDelete = await watchlistRepository.SoftDeleteUserMovieAsync(userMovie);
        if (!successDelete)
            throw new EntityPersistFailureException();
    }

    public async Task<bool> MovieIsInUserWatchlistAsync(string userId, Guid movieId)
    {
        try
        {
            bool userWatchlistEntryExists = await watchlistRepository
                .ExistsAsync(userId, movieId);
            
            return userWatchlistEntryExists;
        }
        catch (NullReferenceException nre)
        {
            throw new EntityKeyNullOrEmptyException(nre.Message);
        }
    }
}