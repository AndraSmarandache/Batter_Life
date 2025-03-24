using Microsoft.AspNetCore.Mvc;
using BatterLife.Models;

namespace BatterLife.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                return View("ContactConfirmation");
            }

            return View(model);
        }

        public IActionResult ContactConfirmation()
        {
            return View();
        }
    }
}