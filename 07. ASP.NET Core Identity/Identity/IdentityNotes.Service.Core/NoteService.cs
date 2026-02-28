namespace IdentityNotes.Service.Core;

using System.Globalization;

using Interfaces;

using Data;
using Data.Models;

using ViewModels.Note;

using static GCommon.ApplicationConstants;

using Microsoft.EntityFrameworkCore;

public class NoteService(NoteDbContext context) : INoteService
{
    public async Task<IEnumerable<NoteListItemViewModel>> GetMyNotesAsync(string userId)
    {
        IEnumerable<NoteListItemViewModel> userNotes =  await context
            .Notes
            .AsNoTracking()
            .Where(n => n.UserId.ToLower() == userId.ToLower())
            .OrderByDescending(n => n.CreatedOn)
            .ThenBy(n => n.Title)
            .Select(n => new NoteListItemViewModel
            {
                Id = n.Id,
                Title = n.Title,
                CreatedOn = n.CreatedOn.ToString(DateTimeFormat, CultureInfo.InvariantCulture)
            })
            .ToArrayAsync();
        
        return userNotes;
    }

    public async Task CreateNoteByUserAsync(CreateNoteViewModel model, string userId)
    {
        Note newNote = new Note
        {
            Title = model.Title,
            Content = model.Content,
            CreatedOn = DateTime.UtcNow,
            UserId = userId
        };
        
        await context.Notes.AddAsync(newNote);
        await context.SaveChangesAsync();
    }

    public async Task<EditNoteViewModel> GetEditNoteModelAsync(int noteId, string userId)
    {
        Note? note = await context
            .Notes
            .AsNoTracking()
            .SingleOrDefaultAsync(n => (n.Id.ToString().ToLower() == noteId.ToString().ToLower()) && 
                                             (n.UserId.ToLower() == userId.ToLower()));
        if (note is null)
            throw new NullReferenceException(NoteNotFoundMessage);

        EditNoteViewModel viewModel = new EditNoteViewModel
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content
        };
        
        return viewModel;
    }
    
    public async Task EditNoteAsync(EditNoteViewModel model, string userId)
    {
        Note? noteToEdit = await context
            .Notes
            .SingleOrDefaultAsync(n => (n.Id.ToString().ToLower() == model.Id.ToString().ToLower()) && 
                                             (n.UserId.ToLower() == userId.ToLower()));
        if (noteToEdit is null)
            throw new NullReferenceException(NoteNotFoundMessage);

        noteToEdit.Title = model.Title;
        noteToEdit.Content = model.Content;

        await context.SaveChangesAsync();
    }

    public async Task<DeleteNoteViewModel> GetDeleteNoteModelAsync(int noteId, string userId)
    {
        Note? note = await context
            .Notes
            .AsNoTracking()
            .SingleOrDefaultAsync(n => (n.Id.ToString().ToLower() == noteId.ToString().ToLower()) && 
                                             (n.UserId.ToLower() == userId.ToLower()));
        if (note is null)
            throw new NullReferenceException(NoteNotFoundMessage);

        DeleteNoteViewModel viewModel = new DeleteNoteViewModel
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content,
            CreatedOn = note.CreatedOn.ToString(DateTimeFormat, CultureInfo.InvariantCulture)
        };
        
        return viewModel;
    }

    public async Task DeleteNoteAsync(DeleteNoteViewModel model, string userId)
    {
        Note? noteToRemove = await context
            .Notes
            .SingleOrDefaultAsync(n => (n.Id.ToString().ToLower() == model.Id.ToString().ToLower()) && 
                                             (n.UserId.ToLower() == userId.ToLower()));
        if (noteToRemove is null)
            throw new NullReferenceException(NoteNotFoundMessage);

        context.Notes.Remove(noteToRemove);
        await context.SaveChangesAsync();
    }

    public async Task<DetailsNoteViewModel> GetNoteDetailsAsync(int noteId, string userId)
    {
        Note? note = await context
            .Notes
            .Include(n => n.User)
            .AsNoTracking()
            .AsSplitQuery()
            .SingleOrDefaultAsync(n => (n.Id.ToString().ToLower() == noteId.ToString().ToLower()) && 
                                             (n.UserId.ToLower() == userId.ToLower()));

        if (note is null)
            throw new NullReferenceException(NoteNotFoundMessage);

        DetailsNoteViewModel viewModel = new DetailsNoteViewModel
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content,
            CreatedOn = note.CreatedOn.ToString(DateTimeFormat, CultureInfo.InvariantCulture),
            CreatedByEmail = note.User.Email!
        };

        return viewModel;
    }
}