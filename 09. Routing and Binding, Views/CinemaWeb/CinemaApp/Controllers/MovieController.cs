namespace CinemaApp.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Core.Contracts;

using ViewModels.Movie;

public class MovieController(IMovieService movieService) : BaseController
{ 
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        IEnumerable<AllMoviesIndexViewModel> allMovies = await movieService.GetAllMovies();
        
        return View(allMovies);
    }
}