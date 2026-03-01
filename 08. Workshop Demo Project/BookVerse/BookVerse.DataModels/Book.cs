namespace BookVerse.DataModels;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

using GCommon.ValidationAttributes;
using static BookVerse.GCommon.ValidationConstants.Book;

public class Book
{
    public int Id { get; set; }

    [Required]
    [MaxLength(IsbnMaxLength)]
    public string Isbn { get; set; } = null!;

    [Required]
    [MaxLength(TitleMax)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(DescriptionMax)]
    public string Description { get; set; } = null!;

    [MaxLength(CoverImageUrlMax)]
    public string? CoverImageUrl { get; set; }

    [Required]
    public DateOnly PublishedOn { get; set; }

    public bool IsDeleted { get; set; } = false;

    [Required]
    public string PublisherId { get; set; } = null!;

    public virtual IdentityUser Publisher { get; set; } = null!;
    
    [Required]
    public int GenreId { get; set; }
    
    public virtual Genre Genre { get; set; } = null!;
    
    public virtual ICollection<UserBook> UsersBooks { get; set; } 
        = new HashSet<UserBook>();
}