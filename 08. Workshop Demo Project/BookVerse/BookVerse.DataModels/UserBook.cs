using System.ComponentModel.DataAnnotations;

namespace BookVerse.DataModels;

using Microsoft.AspNetCore.Identity;

public class UserBook
{
    [Required]
    public string UserId { get; set; } = null!;
        
    public virtual IdentityUser User { get; set; } = null!;

    [Required]
    public int BookId { get; set; }
        
    public virtual Book Book { get; set; } = null!;
}