namespace BookVerse.ViewModels.Book;

using System.ComponentModel.DataAnnotations;

using Genre;

using GCommon.ValidationAttributes;
using static GCommon.ValidationConstants.Book;

public class BookFormModel
{
    [Required]
    [Isbn]
    public string Isbn { get; set; } = null!;
    
    [Required]
    [StringLength(TitleMax, MinimumLength = TitleMin)]
    public string Title { get; set; } = null!;

    public int GenreId { get; set; }

    [Required]
    [StringLength(DescriptionMax, MinimumLength = DescriptionMin)]
    public string Description { get; set; } = null!;

    [Url]
    [MaxLength(CoverImageUrlMax)]
    public string? CoverImageUrl { get; set; }

    public DateTime PublishedOn { get; set; }

    /* ViewModel for HTTP GET, not part of input model */
    public IEnumerable<GenreViewModel> Genres { get; set; }
        = new List<GenreViewModel>();
}
