namespace CinemaApp.Web.Controllers;

using System.Diagnostics;

using ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[AllowAnonymous]
public class HomeController : BaseController
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    [Route("/Home/Error/{statusCode}")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int statusCode)
    {
        return statusCode switch
        {
            StatusCodes.Status400BadRequest => View("BadRequest"),
            StatusCodes.Status404NotFound => View("NotFound"),
            StatusCodes.Status500InternalServerError => View("ServerError"),
            _ => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier })
        };
    }
}