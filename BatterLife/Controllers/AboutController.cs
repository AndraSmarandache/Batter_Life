using Microsoft.AspNetCore.Mvc;

namespace BatterLife.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "About Us - BatterLife";
            return View();
        }
    }
}