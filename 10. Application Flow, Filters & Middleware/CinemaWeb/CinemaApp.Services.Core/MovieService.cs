namespace CinemaApp.Services.Core;

using Contracts;

using Data.Models;
using CinemaApp.Data.Repository.Contracts;

using Web.ViewModels.Movie;

using Models.Movie;
using AutoMapper;

using GCommon.Exceptions;

public class MovieService(IMovieRepository movieRepository, IMapper mapper) : IMovieService
{
    public async Task<IEnumerable<MovieAllDto>> GetAllMoviesOrderedAsync()
    {
        // Fetch data
        IEnumerable<Movie> allMoviesDb = await movieRepository.GetAllMoviesNoTrackingWithProjectionAsync(m => new Movie
            {
                Id = m.Id,
                Title = m.Title,
                Genre = m.Genre,
                Director = m.Director,
                ReleaseDate = m.ReleaseDate,
                ImageUrl = m.ImageUrl,
            });
        
        // Process data
        IEnumerable<MovieAllDto> allMoviesViewModel = mapper
            .Map<IEnumerable<MovieAllDto>>(allMoviesDb)
            .OrderBy(m => m.Title)
            .ThenBy(m => m.Director)
            .ThenBy(m => m.Genre)
            .ToArray();

        // Return data
        return allMoviesViewModel;
    }

    public async Task CreateMovieAsync(MovieDetailsDto movieDetailsDto)
    {
        Movie newMovie = mapper.Map<Movie>(movieDetailsDto);

        bool successAdd = await movieRepository.AddMovieAsync(newMovie);
        
        if (!successAdd)
            throw new EntityPersistFailureException();
    }

    public async Task<MovieDetailsDto?> GetMovieDetailsByIdAsync(Guid movieId)
    {
        Movie? movieDb = await movieRepository.GetMovieByIdAsync(movieId);

        if (movieDb is null)
            return null;

        return mapper.Map<MovieDetailsDto>(movieDb);
    }

    public async Task<MovieDetailsDto?> GetMovieFormModelByIdAsync(Guid movieId)
    {
        Movie? movieDb = await movieRepository.GetMovieByIdAsync(movieId);

        if (movieDb is null)
            return null;

        return mapper.Map<MovieDetailsDto>(movieDb);
    }

    public async Task<bool> ExistsByIdAsync(Guid movieId)
    {
        return await movieRepository.ExistsByIdAsync(movieId);
    }

    public async Task EditMovieAsync(Guid movieId, MovieDetailsDto movieDetailsDto)
    {
        Movie? movieDb = await movieRepository.GetMovieByIdAsync(movieId);

        if (movieDb is null)
            throw new EntityNotFoundException();
        
        movieDb.Title = movieDetailsDto.Title;
        movieDb.Genre = movieDetailsDto.Genre;
        movieDb.ReleaseDate = movieDetailsDto.ReleaseDate;
        movieDb.Description = movieDetailsDto.Description;
        movieDb.Duration = movieDetailsDto.Duration;
        movieDb.Director = movieDetailsDto.Director;
        movieDb.ImageUrl = movieDetailsDto.ImageUrl;

        bool successUpdate = await movieRepository.UpdateMovieAsync(movieDb);
        
        if (!successUpdate)
            throw new EntityPersistFailureException();
    }

    public async Task SoftDeleteMovieAsync(Guid movieId)
    {
        Movie? movieDb = await movieRepository.GetMovieByIdAsync(movieId);
        
        if (movieDb is null)
            throw new EntityNotFoundException();
        
        bool successDelete = await movieRepository.SoftDeleteMovieAsync(movieDb);
        
        if (!successDelete)
            throw new EntityPersistFailureException();
    }

    public async Task HardDeleteMovieAsync(Guid movieId)
    {
        Movie? movieDb = await movieRepository.GetMovieByIdAsync(movieId);
        
        if (movieDb is null)
            throw new EntityNotFoundException();
        
        bool successDelete = await movieRepository.HardDeleteMovieAsync(movieDb);
        
        if (!successDelete)
            throw new EntityPersistFailureException();
    }
}
