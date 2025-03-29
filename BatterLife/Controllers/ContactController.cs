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
        [ValidateAntiForgeryToken]
        public IActionResult Submit(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Confirmation");
            }

            return View("Index", model);
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}