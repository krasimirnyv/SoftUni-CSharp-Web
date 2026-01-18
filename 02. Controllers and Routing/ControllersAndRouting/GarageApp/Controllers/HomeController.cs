namespace GarageApp.Controllers;

using System.Diagnostics;

using ViewModels;

using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> logger;

    public HomeController(ILogger<HomeController> logger)
    {
        this.logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
    
    [HttpGet]
    [Route("Error/{statusCode:int}")]
    public IActionResult StatusCodeError(int statusCode)
    {
        return statusCode switch
        {
            404 => View("NotFound"),
            400 => View("BadRequest"),
            _ => View("Error")
        };
    }
}