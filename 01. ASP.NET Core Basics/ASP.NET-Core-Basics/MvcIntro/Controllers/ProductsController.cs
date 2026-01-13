using Microsoft.AspNetCore.Mvc;

namespace MvcIntro.Controllers;

public class ProductsController : Controller
{
    // GET
    public IActionResult Index()
    {
        ViewBag.PageTitle = "My products (ViewBag Demo)";
        return View();
    }

    [Route("Products/{id}")]
    [Route("Products/Details/{id}")]
    public IActionResult Details(int id)
    {
        return id <= 0 ? Json("Invalid product id") : Ok($"Product details for product with id: {id}");
    }
}
