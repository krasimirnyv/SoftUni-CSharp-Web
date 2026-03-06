namespace CinemaApp.Web.Controllers;

using ViewModels.Watchlist;
using Services.Core.Contracts;
using Services.Models.Watchlist;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using GCommon.Exceptions;

using static GCommon.OutputMessages.Watchlist;
using static GCommon.ApplicationConstants;

public class WatchlistController(IWatchlistService watchlistService, IMapper mapper, ILogger<WatchlistController> logger) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        string userId = GetUserId()!;
        
        IEnumerable<WatchlistMovieDto> watchlistMovieDtos = await watchlistService
            .GetUserWatchlistByIdAsync(userId);
        
        IEnumerable<WatchlistMovieViewModel> watchlistMovieViewModels = mapper
            .Map<IEnumerable<WatchlistMovieViewModel>>(watchlistMovieDtos);
        
        return View(watchlistMovieViewModels);
    }

    [HttpGet]
    public async Task<IActionResult> Add(Guid id)
    {
        string userId = GetUserId()!;

        try
        {
            await watchlistService.AddMovieToUserWatchlistAsync(userId, id);
            TempData[SuccessTempDataKey] = "Added new movie to your watchlist";
            
            return RedirectToAction(nameof(Index));
        }
        catch (EntityAlreadyExistsException eaee)
        {
            logger.LogError(eaee, string.Format(MovieAlreadyInWatchlist, id, userId));
            return BadRequest(eaee.Message);
        }
        catch (EntityNotFoundException enfe)
        {
            logger.LogError(enfe, string.Format(MovieNotFoundMessage, "add"));
            return NotFound(enfe.Message);
        }
        catch (EntityPersistFailureException epfe)
        {
            logger.LogError(epfe, AddToWatchlistFailureMessage);
            TempData[ErrorTempDateKey] = AddToWatchlistFailureMessage;

            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, UnexpectedErrorMessage);
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Remove(Guid movieId)
    {
        string userId = GetUserId()!;

        try
        {
            await watchlistService.RemoveMovieFromUserWatchlistAsync(userId, movieId);
            TempData[SuccessTempDataKey] = "Removed the movie from your watchlist";

            return RedirectToAction(nameof(Index));
        }
        catch (EntityNotFoundException enfe)
        {
            logger.LogError(enfe, string.Format(MovieNotFoundMessage, "remove"));
            return BadRequest(enfe.Message);
        }
        catch (EntityPersistFailureException epfe)
        {
            logger.LogError(epfe, RemoveFromWatchlistFailureMessage);
            TempData[ErrorTempDateKey] = RemoveFromWatchlistFailureMessage;
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, UnexpectedErrorMessage);
            return BadRequest(ex.Message);
        }
    }
}