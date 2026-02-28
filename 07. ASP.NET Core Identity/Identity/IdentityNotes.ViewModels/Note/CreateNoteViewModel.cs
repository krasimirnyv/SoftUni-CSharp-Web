namespace IdentityNotes.ViewModels.Note;

using System.ComponentModel.DataAnnotations;

using static GCommon.EntityValidations;

public class CreateNoteViewModel
{
    [Required]
    [StringLength(NoteTitleMaxLength, MinimumLength = NoteTitleMinLength)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(NoteContentMaxLenght, MinimumLength = NoteContentMinLenght)]
    public string Content { get; set; } = null!;
}