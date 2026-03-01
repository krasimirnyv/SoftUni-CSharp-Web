namespace BookVerse.Services.Core.Contracts;

using ViewModels.Genre;
using ViewModels.Book;

public interface IGenreService
{
    Task<IEnumerable<GenreViewModel>> GetAllGenresUnorderedAsync();
    
    Task<IEnumerable<GenreViewModel>> GetAllGenresOrderedByNameAsync();
    
    Task<IEnumerable<GenreViewModel>> GetAllGenresOrderedByNameAsync(BookEditInputModel model);

    Task<bool> IsExistByIdAsync(int id);
}