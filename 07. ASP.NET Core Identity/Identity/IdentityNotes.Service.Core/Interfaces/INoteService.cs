namespace IdentityNotes.Service.Core.Interfaces;

using ViewModels.Note;

public interface INoteService
{
    Task<IEnumerable<NoteListItemViewModel>> GetMyNotesAsync(string userId);
    
    Task CreateNoteByUserAsync(CreateNoteViewModel model, string userId);
    
    Task<EditNoteViewModel> GetEditNoteModelAsync(int noteId, string userId);
    
    Task EditNoteAsync(EditNoteViewModel model, string userId);
    
    Task<DeleteNoteViewModel> GetDeleteNoteModelAsync(int noteId, string userId);
    
    Task DeleteNoteAsync(DeleteNoteViewModel model, string userId);
    
    Task<DetailsNoteViewModel> GetNoteDetailsAsync(int noteId, string userId);
}
