namespace CinemaApp.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Core.Contracts;

using ViewModels.Movie;

using GCommon.Exceptions;
using static GCommon.OutputMessages.Movie;
using static GCommon.ApplicationConstants;

public class MovieController(IMovieService movieService, ILogger<MovieController> logger) : BaseController
{ 
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        IEnumerable<AllMoviesIndexViewModel> allMovies = await movieService.GetAllMovies();
        
        return View(allMovies);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(MovieFormModel formModel)
    {
        if (!ModelState.IsValid)
        {
            return View(formModel);
        }

        try
        {
            await movieService.CreateMovieAsync(formModel);
        }
        catch (EntityCreatePersistFailureException ecpfe)
        {
            logger.LogError(ecpfe, CreateMovieFailureMessage);
            ModelState.AddModelError(string.Empty, CreateMovieFailureMessage);
            return View(formModel);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, UnexpectedErrorMessage);
            ModelState.AddModelError(string.Empty, UnexpectedErrorMessage);
            
            // TODO: Redirect after implementing Notifications
            return View(formModel);
        }
        
        // TODO: Redirect to Manage after implementing Roles
        return RedirectToAction(nameof(Index));
    }
}