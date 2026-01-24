namespace BookShelf.Models;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidation;

public class Author
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(AuthorNameMaxLength)]
    public string Name { get; set; } = null!;
    
    [MaxLength(AuthorCountryMaxLength)]
    public string? Country { get; set; }
    
    public virtual ICollection<Book> Books { get; set; }
        = new HashSet<Book>();
}
