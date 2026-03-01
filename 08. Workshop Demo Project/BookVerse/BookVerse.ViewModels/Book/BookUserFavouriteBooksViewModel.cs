namespace BookVerse.ViewModels.Book;

public class BookUserFavouriteBooksViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? CoverImageUrl { get; set; }

    public string GenreName { get; set; } = null!;
}
