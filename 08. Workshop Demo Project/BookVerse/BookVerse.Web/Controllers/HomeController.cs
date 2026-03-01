namespace BookVerse.Web.Controllers;

using System.Diagnostics;

using ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[AllowAnonymous]
public class HomeController : BaseController
{
    [HttpGet]
    public IActionResult Index()
    {
        if (IsAuthenticated())
            return RedirectToAction("Index", "Book");
        
        return View();
    }

    [HttpGet]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}