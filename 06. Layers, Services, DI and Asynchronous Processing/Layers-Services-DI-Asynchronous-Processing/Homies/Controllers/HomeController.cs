namespace Homies.Controllers
{
    using System.Diagnostics;

    using ViewModels;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated ?? false)
                return RedirectToAction("All", "Event");
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}