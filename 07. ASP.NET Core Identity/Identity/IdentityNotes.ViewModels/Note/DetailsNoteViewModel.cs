namespace IdentityNotes.ViewModels.Note;

public class DetailsNoteViewModel : NoteListItemViewModel
{
    public string Content { get; set; } = null!;
    
    public string CreatedByEmail { get; set; } = null!;
}