namespace IdentityNotes.ViewModels.Note;

using System.ComponentModel.DataAnnotations;

public class EditNoteViewModel : CreateNoteViewModel
{
    [Required]
    public int Id { get; set; }
}