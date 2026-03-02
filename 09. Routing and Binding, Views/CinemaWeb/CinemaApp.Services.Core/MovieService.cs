namespace CinemaApp.Services.Core;

using System.Globalization;

using Microsoft.EntityFrameworkCore;

using Contracts;

using Data;

using Web.ViewModels.Movie;

using static GCommon.ApplicationConstants;

public class MovieService(CinemaDbContext context) : IMovieService
{
    public async Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMovies()
    {
        IEnumerable<AllMoviesIndexViewModel> allMovies = await context
            .Movies
            .AsNoTracking()
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
    
}