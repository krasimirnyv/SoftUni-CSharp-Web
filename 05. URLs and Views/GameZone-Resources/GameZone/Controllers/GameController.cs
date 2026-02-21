namespace GameZone.Controllers
{
    using System.Diagnostics;
    using System.Globalization;

    using Data;
    using Data.Models;
    using ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Authorization;

    using static Common.ValidationConstants;
    
    [AllowAnonymous]
    public class GameController(GameDbContext context,
                                ILogger<GameController> logger) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                IEnumerable<GameViewModel> games = await context
                    .Games
                    .Include(g => g.Genre)
                    .AsNoTracking()
                    .OrderBy(g => g.Title)
                    .ThenByDescending(g => g.ReleasedOn)
                    .ThenBy(g => g.PublisherName)
                    .ThenBy(g => g.Genre.Name)
                    .Select(g => new GameViewModel
                    {
                        Id = g.Id.ToString(),
                        Title = g.Title,
                        ImageUrl = g.ImageUrl,
                        PublisherName = g.PublisherName,
                        ReleasedOn = g.ReleasedOn.ToString(DateFormat, CultureInfo.InvariantCulture),
                        Genre = g.Genre.Name
                    })
                    .ToArrayAsync();

                if (!games.Any())
                    logger.LogInformation("No games found.");
                
                return View(games);
            }
            catch (Exception e)
            { 
                logger.LogError(e, "An error occurred while retrieving games."); 
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            bool isIdInt = int.TryParse(id, out int gameId);
            if (!isIdInt || gameId <= 0)
            {
                logger.LogWarning("Invalid game ID: {Id}", id);
                return BadRequest();
            }
            
            try
            {
                Game? selectedGame = await context
                    .Games
                    .Include(g => g.Genre)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(g => g.Id == gameId);

                if (selectedGame == null)
                {
                    logger.LogWarning("Game with ID {Id} not found.", id);
                    return NotFound();
                }

                GameDetailsViewModel? game = new GameDetailsViewModel
                {
                    Id = selectedGame.Id.ToString(),
                    Title = selectedGame.Title,
                    Description = selectedGame.Description,
                    ImageUrl = selectedGame.ImageUrl,
                    PublisherName = selectedGame.PublisherName,
                    ReleasedOn = selectedGame.ReleasedOn.ToString(DateFormat, CultureInfo.InvariantCulture),
                    Genre = selectedGame.Genre.Name
                };

                return View(game);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while retrieving game details for ID {Id}.", id);
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            GameAddInputModel model = new GameAddInputModel()
            {
                Genres = await GetAllGenresAsync()
            };
            
           return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(GameAddInputModel inputModel)
        {
            // Genres can be stored hidden in the form as a select list
            inputModel.Genres = await GetAllGenresAsync();

            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid game input model.");
                return View(inputModel);
            }

            if (!await GenreExists(inputModel.GenreId))
            {
                ModelState.AddModelError(nameof(inputModel.GenreId), "Invalid Genre is selected!");
                return View(inputModel);
            }

            try
            {
                Game newGame = new Game
                {
                    Title = inputModel.Title,
                    Description = inputModel.Description,
                    ImageUrl = inputModel.ImageUrl,
                    PublisherName = inputModel.PublisherName,
                    ReleasedOn = inputModel.ReleasedOn,
                    GenreId = inputModel.GenreId
                };

                await context.Games.AddAsync(newGame);
                await context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Game \"{newGame.Title}\" is added successfully!";
                return RedirectToAction("All");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while adding a new game.");
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while adding the game. Please try again later.");
                return View(inputModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool isIdInt = int.TryParse(id, out int gameId);
            if (!isIdInt || gameId <= 0)
            {
                logger.LogWarning("Invalid game ID: {Id}", id);
                return BadRequest();
            }
            
            Game? selectedGame = await context
                .Games
                .AsNoTracking()
                .SingleOrDefaultAsync(g => g.Id == gameId);

            if (selectedGame == null)
            {
                logger.LogWarning("Game with ID {Id} not found.", id);
                return NotFound();
            }
            
            GameEditInputModel model = new GameEditInputModel()
            {
                Title = selectedGame.Title,
                Description = selectedGame.Description,
                ImageUrl = selectedGame.ImageUrl,
                PublisherName = selectedGame.PublisherName,
                ReleasedOn = selectedGame.ReleasedOn,
                GenreId = selectedGame.GenreId,
                Genres = await GetAllGenresAsync()
            };
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, GameEditInputModel inputModel)
        {
            bool isIdInt = int.TryParse(id, out int gameId);
            if (!isIdInt || gameId <= 0)
            {
                logger.LogWarning("Invalid game ID: {Id}", id);
                return BadRequest();
            }
            
            Game? selectedGame = await context
                .Games
                .SingleOrDefaultAsync(g => g.Id == gameId);

            if (selectedGame == null)
            {
                logger.LogWarning("Game with ID {Id} not found.", id);
                return NotFound();
            }
            
            // Genres can be stored hidden in the form as a select list
            inputModel.Genres = await GetAllGenresAsync();

            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid game input model.");
                return View(inputModel);
            }

            if (!await GenreExists(inputModel.GenreId))
            {
                ModelState.AddModelError(nameof(inputModel.GenreId), "Invalid Genre is selected!");
                return View(inputModel);
            }
            
            try
            {
                selectedGame.Title = inputModel.Title;
                selectedGame.Description = inputModel.Description;
                selectedGame.ImageUrl = inputModel.ImageUrl;
                selectedGame.PublisherName = inputModel.PublisherName;
                selectedGame.ReleasedOn = inputModel.ReleasedOn;
                selectedGame.GenreId = inputModel.GenreId;
                
                context.Games.Update(selectedGame);
                await context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Game \"{selectedGame.Title}\" is updated successfully!";
                return RedirectToAction("All");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while updating the game.");
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while updating the game. Please try again later.");
                return View(inputModel);
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool isIdInt = int.TryParse(id, out int gameId);
            if (!isIdInt || gameId <= 0)
            {
                logger.LogWarning("Invalid game ID: {Id}", id);
                return BadRequest();
            }
            
            Game? selectedGame = await context
                .Games
                .AsNoTracking()
                .SingleOrDefaultAsync(g => g.Id == gameId);

            if (selectedGame == null)
            {
                logger.LogWarning("Game with ID {Id} not found.", id);
                return NotFound();
            }
            
            GameDeleteViewModel model = new GameDeleteViewModel()
            {
                Id = selectedGame.Id.ToString(),
                Title = selectedGame.Title,
            };
            
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(string id, GameDeleteViewModel model)
        {
             bool isIdInt = int.TryParse(id, out int gameId);
            if (!isIdInt || gameId <= 0)
            {
                logger.LogWarning("Invalid game ID: {Id}", id);
                return BadRequest();
            }
            
            Game? selectedGame = await context
                .Games
                .SingleOrDefaultAsync(g => g.Id == gameId);

            if (selectedGame == null)
            {
                logger.LogWarning("Game with ID {Id} not found.", id);
                return NotFound();
            }
            
            try
            {
                context.Games.Remove(selectedGame);
                await context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Game \"{selectedGame.Title}\" is deleted successfully!";
                return RedirectToAction("All");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while deleting the game.");
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while deleting the game. Please try again later.");
                return View(model);
            }
        }
        
        private async Task<IEnumerable<GenreViewModel>> GetAllGenresAsync()
        {
            return await context
                .Genres
                .AsNoTracking()
                .Select(g => new GenreViewModel
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .OrderBy(g => g.Name)
                .ToArrayAsync();
        }

        private async Task<bool> GenreExists(int id)
        {
            return await context.Genres.AnyAsync(g => g.Id == id);
        }
    }
}
