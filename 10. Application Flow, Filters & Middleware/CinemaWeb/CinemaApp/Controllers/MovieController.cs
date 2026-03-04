namespace CinemaApp.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Core.Contracts;
using Services.Models.Movie;

using ViewModels.Movie;
using AutoMapper;

using GCommon.Exceptions;
using static GCommon.OutputMessages.Movie;
using static GCommon.ApplicationConstants;

public class MovieController(IMovieService movieService, IMapper mapper, ILogger<MovieController> logger) : BaseController
{ 
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        IEnumerable<MovieAllDto> movieAllDtos = await movieService.GetAllMoviesOrderedAsync();
        
        IEnumerable<AllMoviesIndexViewModel> allMoviesIndex = mapper.Map<IEnumerable<AllMoviesIndexViewModel>>(movieAllDtos);
            
        return View(allMoviesIndex);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Details(Guid id)
    {
        if (id == Guid.Empty)
            return NotFound();
        
        MovieDetailsDto? movieDetailsDto = await movieService.GetMovieDetailsByIdAsync(id);
        
        if (movieDetailsDto is null)
            return NotFound();
        
        MovieDetailsViewModel movieDetailsView = mapper.Map<MovieDetailsViewModel>(movieDetailsDto);
        
        return View(movieDetailsView);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(MovieFormModel movieFormModel)
    {
        if (!ModelState.IsValid)
            return View(movieFormModel);

        try
        {
            MovieDetailsDto movieDetailsDto = mapper.Map<MovieDetailsDto>(movieFormModel);
            
            await movieService.CreateMovieAsync(movieDetailsDto);
        }
        catch (EntityPersistFailureException epfe)
        {
            logger.LogError(epfe, string.Format(CrudMovieFailureMessage, nameof(Create)));
            ModelState.AddModelError(string.Empty, string.Format(CrudMovieFailureMessage, "creating"));
            return View(movieFormModel);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, UnexpectedErrorMessage);
            ModelState.AddModelError(string.Empty, UnexpectedErrorMessage);
            
            // TODO: Redirect after implementing Notifications
            return View(movieFormModel);
        }
        
        // TODO: Redirect to Manage after implementing Roles
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        if (id == Guid.Empty)
            return NotFound();
        
        MovieDetailsDto? movieDetailsDto = await movieService.GetMovieFormModelByIdAsync(id);

        if (movieDetailsDto is null)
            return NotFound();
        
        MovieFormModel movieFormModel = mapper.Map<MovieFormModel>(movieDetailsDto);
        
        return View(movieFormModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit([FromRoute] Guid id, MovieFormModel movieFormModel)
    {
        if (id == Guid.Empty)
            return NotFound();
        
        if (!ModelState.IsValid)
            return View(movieFormModel);

        try
        {
            MovieDetailsDto movieDetailsDto = mapper.Map<MovieDetailsDto>(movieFormModel);
            
            await movieService.EditMovieAsync(id, movieDetailsDto);
        }
        catch (EntityNotFoundException enfe)
        {
            return NotFound(enfe.Message);
        }
        catch (EntityPersistFailureException epfe)
        {
            logger.LogError(epfe, string.Format(CrudMovieFailureMessage, nameof(Edit)));
            ModelState.AddModelError(string.Empty, string.Format(CrudMovieFailureMessage, "editing"));
            return View(movieFormModel);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, UnexpectedErrorMessage);
            ModelState.AddModelError(string.Empty, UnexpectedErrorMessage);
            
            // TODO: Redirect after implementing Notifications
            return View(movieFormModel);
        }
        
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (id == Guid.Empty)
            return NotFound();

        MovieDetailsDto? movieDetailsDto = await movieService.GetMovieDetailsByIdAsync(id);
        
        if (movieDetailsDto is null)
            return NotFound();
        
        MovieDeleteViewModel movieDeleteView = mapper.Map<MovieDeleteViewModel>(movieDetailsDto);

        return View(movieDeleteView);
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(Guid id, MovieDetailsViewModel? movieDetails)
    {
        if (id == Guid.Empty)
            return NotFound();

        try
        {
            await movieService.SoftDeleteMovieAsync(id);
        }
        catch (EntityNotFoundException enfe)
        {
            return NotFound(enfe.Message);
        }
        catch (EntityPersistFailureException epfe)
        {
            logger.LogError(epfe, string.Format(CrudMovieFailureMessage, nameof(Delete)));
            ModelState.AddModelError(string.Empty, string.Format(CrudMovieFailureMessage, "deleting"));
            return View(movieDetails);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, UnexpectedErrorMessage);
            ModelState.AddModelError(string.Empty, UnexpectedErrorMessage);
            return View(movieDetails);
        }
        
        return RedirectToAction(nameof(Index));
    }
}