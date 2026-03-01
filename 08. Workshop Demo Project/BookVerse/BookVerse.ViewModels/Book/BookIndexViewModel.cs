namespace BookVerse.ViewModels.Book;

public class BookIndexViewModel : BookUserFavouriteBooksViewModel
{
    public int SavedCount { get; set; }

    public bool IsAuthor { get; set; }

    public bool IsSaved { get; set; }
}
