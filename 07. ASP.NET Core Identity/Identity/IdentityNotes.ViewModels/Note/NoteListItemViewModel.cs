namespace IdentityNotes.ViewModels.Note;

public class NoteListItemViewModel
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string CreatedOn { get; set; } = null!;
}