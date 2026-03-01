namespace BookVerse.DataModels;

using System.ComponentModel.DataAnnotations;
    
using static BookVerse.GCommon.ValidationConstants.Genre;
    
public class Genre
{
    public int Id { get; set; }

    [Required]
    [MaxLength(NameMax)]
    public string Name { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; }
        = new HashSet<Book>();
}