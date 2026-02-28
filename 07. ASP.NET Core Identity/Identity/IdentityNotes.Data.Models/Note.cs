namespace IdentityNotes.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;

using static GCommon.EntityValidations;
using static GCommon.ApplicationConstants;

public class Note
{
    public int Id { get; set; }

    [Required]
    [MaxLength(NoteTitleMaxLength)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(NoteContentMaxLenght)]
    public string Content { get; set; } = null!;

    [Column(TypeName = DateTimeColumnType)]
    public DateTime CreatedOn { get; set; }

    [Required]
    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = null!;

    public virtual IdentityUser User { get; set; } = null!;

}