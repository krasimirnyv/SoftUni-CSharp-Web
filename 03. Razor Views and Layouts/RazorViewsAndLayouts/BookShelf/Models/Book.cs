namespace BookShelf.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static Common.EntityValidation;

public class Book
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(BookTitleMaxLength)]
    public string Title { get; set; } = null!;
    
    public int Year { get; set; }
    
    [Required]
    [ForeignKey(nameof(Author))]
    public Guid AuthorId { get; set; }
    
    public virtual Author Author { get; set; } = null!;
}
