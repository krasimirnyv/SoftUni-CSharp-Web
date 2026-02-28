namespace IdentityNotes.Web.Controllers;

using Microsoft.AspNetCore.Mvc;

using ViewModels.Note;

using Service.Core.Interfaces;

using static GCommon.ApplicationConstants;

public class NoteController(INoteService service, ILogger<NoteController> logger) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> MyNotes()
    {
        string? userId = GetUserId();
        if (userId is null)
            return RedirectToAction("Index", "Home");

        IEnumerable<NoteListItemViewModel> userNotes = await service.GetMyNotesAsync(userId);
        
        return View(userNotes);
    }

    [HttpGet]
    public IActionResult Create()
        => View(new CreateNoteViewModel());
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateNoteViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        string? userId = GetUserId();
        if (userId is null)
            return RedirectToAction("Index", "Home");
        
        try
        {
            await service.CreateNoteByUserAsync(model, userId);
            return RedirectToAction("MyNotes");
        }
        catch (Exception e)
        {
            logger.LogError(e, ErrorForCreatingNote);
            return BadRequest();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int noteId)
    {
        string? userId = GetUserId();
        if (userId is null)
            return RedirectToAction("Index", "Home");

        try
        {
            EditNoteViewModel viewModel = await service.GetEditNoteModelAsync(noteId, userId);
            return View(viewModel);
        }
        catch (NullReferenceException e)
        {
            ModelState.AddModelError(string.Empty, NoteNotFoundMessage);
            logger.LogError(e, ErrorForLoadingUpdatePageNote + " " + NoteNotFoundMessage);
            return RedirectToAction("MyNotes");
        }
        catch (Exception e)
        {
            logger.LogError(e, ErrorForLoadingUpdatePageNote);
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditNoteViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);
        
        string? userId = GetUserId();
        if (userId is null)
            return RedirectToAction("Index", "Home");

        try
        {
            await service.EditNoteAsync(model, userId);
        }
        catch (NullReferenceException e)
        {
            ModelState.AddModelError(string.Empty, NoteNotFoundMessage);
            logger.LogError(e, ErrorForUpdatingNote + " " + NoteNotFoundMessage);
        }
        catch (Exception e)
        {
            logger.LogError(e, ErrorForUpdatingNote); 
            return BadRequest();
        }
        
        return RedirectToAction("MyNotes");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int noteId)
    {
        string? userId = GetUserId();
        if (userId is null)
            return RedirectToAction("Index", "Home");

        try
        {
            DeleteNoteViewModel viewModel = await service.GetDeleteNoteModelAsync(noteId, userId);
            return View(viewModel);
        }
        catch (NullReferenceException e)
        {
            ModelState.AddModelError(string.Empty, NoteNotFoundMessage);
            logger.LogError(e, ErrorForLoadingDeletePageNote + " " + NoteNotFoundMessage);
            return RedirectToAction("MyNotes");
        }
        catch (Exception e)
        {
            logger.LogError(e, ErrorForLoadingDeletePageNote);
            return BadRequest();
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(DeleteNoteViewModel model)
    {
        string? userId = GetUserId();
        if (userId is null)
            return RedirectToAction("Index", "Home");
        
        try
        {
            await service.DeleteNoteAsync(model, userId);
        }
        catch (NullReferenceException e)
        {
            ModelState.AddModelError(string.Empty, NoteNotFoundMessage);
            logger.LogError(e, ErrorForDeletingNote + " " + NoteNotFoundMessage);
        }
        catch (Exception e)
        {
            logger.LogError(e, ErrorForDeletingNote); 
            return BadRequest();
        }
        
        return RedirectToAction("MyNotes");
    }

    [HttpGet]
    public async Task<IActionResult> Details(int noteId)
    {
        string? userId = GetUserId();
        if (userId is null)
            return RedirectToAction("Index", "Home");

        try
        {
            DetailsNoteViewModel viewModel = await service.GetNoteDetailsAsync(noteId, userId);
            return View(viewModel);
        }
        catch (NullReferenceException e)
        {
            ModelState.AddModelError(string.Empty, NoteNotFoundMessage);
            logger.LogError(e, ErrorForLoadingDetailsPageNote + " " + NoteNotFoundMessage);
            return RedirectToAction("MyNotes");
        }
        catch (Exception e)
        {
            logger.LogError(e, ErrorForLoadingDetailsPageNote); 
            return BadRequest();
        }
    }
}
