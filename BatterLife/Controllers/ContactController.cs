using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BatterLife.Models;
using BatterLife.Services.Interfaces;

namespace BatterLife.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                await _contactService.AddMessageAsync(model);
                return RedirectToAction("Confirmation");
            }

            return View("Index", model);
        }

        public IActionResult Confirmation()
        {
            return View();
        }

        public async Task<IActionResult> Messages()
        {
            var messages = await _contactService.GetAllMessagesAsync();
            return View(messages);
        }
    }
}