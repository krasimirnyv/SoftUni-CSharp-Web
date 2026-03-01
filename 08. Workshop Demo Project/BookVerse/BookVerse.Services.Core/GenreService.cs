namespace BookVerse.Services.Core;

using Data;

using Contracts;

using ViewModels.Genre;
using ViewModels.Book;

using Microsoft.EntityFrameworkCore;

public class GenreService(BookVerseDbContext context) : IGenreService
{
    public async Task<IEnumerable<GenreViewModel>> GetAllGenresUnorderedAsync()
    {
        IEnumerable<GenreViewModel> allGenres = await context
            .Genres
            .AsNoTracking()
            .Select(g => new GenreViewModel
            {
                Id = g.Id,
                Name = g.Name
            })
            .ToArrayAsync();
        
        return allGenres;    
    }

    public async Task<IEnumerable<GenreViewModel>> GetAllGenresOrderedByNameAsync()
    {
        IEnumerable<GenreViewModel> allGenres = await context
            .Genres
            .AsNoTracking()
            .Select(g => new GenreViewModel
            {
                Id = g.Id,
                Name = g.Name
            })
            .OrderBy(g => g.Name)
            .ToArrayAsync();
        
        return allGenres;
    }
    
    public async Task<IEnumerable<GenreViewModel>> GetAllGenresOrderedByNameAsync(BookEditInputModel model)
    {
        IEnumerable<GenreViewModel> allGenres = await context
            .Genres
            .AsNoTracking()
            .Where(g => g.Name.ToLower() != model.GenreName.ToLower())
            .Select(g => new GenreViewModel
            {
                Id = g.Id,
                Name = g.Name
            })
            .OrderBy(g => g.Name)
            .ToArrayAsync();
        
        return allGenres;
    }

    public async Task<bool> IsExistByIdAsync(int id)
    {
        bool genreExists = await context
            .Genres
            .AsNoTracking()
            .AnyAsync(g => g.Id == id);
        
        return genreExists;
    }
}