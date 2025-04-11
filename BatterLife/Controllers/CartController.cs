using BatterLife.Models;
using BatterLife.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BatterLife.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly BatterLifeDbContext _context; 

        public CartController(ICartService cartService, BatterLifeDbContext context) 
        {
            _cartService = cartService;
            _context = context; 
        }

        public async Task<IActionResult> Index()
        {
            var sessionId = GetSessionId();
            var cart = await _cartService.GetOrCreateCartAsync(sessionId);
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(int productId, int quantity)
        {
            var sessionId = HttpContext.Session.Id;
            var product = await _context.Products.FindAsync(productId); 

            if (product == null)
            {
                return Json(new { success = false, message = "Product not found" });
            }

            await _cartService.AddToCartAsync(sessionId, productId, quantity);

            return Json(new
            {
                success = true,
                message = $"{quantity} {product.Name}(s) added to cart"
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateItem(int productId, int quantity)
        {
            var sessionId = GetSessionId();
            await _cartService.UpdateCartItemAsync(sessionId, productId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            var sessionId = GetSessionId();
            await _cartService.RemoveFromCartAsync(sessionId, productId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var sessionId = GetSessionId();
            await _cartService.ClearCartAsync(sessionId);
            return View("OrderConfirmation");
        }

        private string GetSessionId()
        {
            return HttpContext.Session.Id;
        }
    }
}