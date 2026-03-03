namespace CinemaApp.Services.Core;

using System.Globalization;

using Microsoft.EntityFrameworkCore;

using Contracts;

using Data.Models;
using CinemaApp.Data.Repository.Contracts;

using Web.ViewModels.Movie;

using GCommon.Exceptions;
using static GCommon.ApplicationConstants;

public class MovieService(IMovieRepository movieRepository) : IMovieService
{
    public async Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMovies()
    {
        IEnumerable<AllMoviesIndexViewModel> allMovies = await movieRepository
            .GetAllMoviesNoTracking()
            .OrderBy(m => m.ReleaseDate)
            .ThenBy(m => m.Title)
            .ThenBy(m => m.Director)
            .ThenBy(m => m.Genre)
            .Select(m => new AllMoviesIndexViewModel
            {
                Id = m.Id,
                ImageUrl = m.ImageUrl ?? DefaultImageUrl,
                Title = m.Title,
                Genre = m.Genre,
                Director = m.Director,
                ReleaseDate = m.ReleaseDate.ToString(DefaultDateFormat, CultureInfo.InvariantCulture)
            })
            .ToArrayAsync();

        return allMovies;
    }

    public async Task CreateMovieAsync(MovieFormModel formModel)
    {
        Movie newMovie = new Movie()
        {
            Title = formModel.Title,
            Genre = formModel.Genre,
            ReleaseDate = DateOnly.FromDateTime(formModel.ReleaseDate),
            Description = formModel.Description,
            Duration = formModel.Duration,
            Director = formModel.Director,
            ImageUrl = formModel.ImageUrl,
        };

        bool successAdd = await movieRepository.AddMovieAsync(newMovie);
        
        if (!successAdd)
            throw new EntityCreatePersistFailureException();
    }
}
