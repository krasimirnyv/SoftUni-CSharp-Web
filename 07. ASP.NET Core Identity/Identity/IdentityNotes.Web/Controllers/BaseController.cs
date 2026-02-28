namespace IdentityNotes.Web.Controllers;

using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public abstract class BaseController : Controller
{
    protected string? GetUserId()
        => User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? null;
    
    protected bool IsAuthenticated()
        => User.Identity?.IsAuthenticated ?? false;
}