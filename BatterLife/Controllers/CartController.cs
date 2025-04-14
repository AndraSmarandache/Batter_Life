using BatterLife.Models;
using BatterLife.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BatterLife.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] CartItemRequest request)
        {
            HttpContext.Session.SetString("Init", "1");
            var sessionId = HttpContext.Session.Id;

            var result = await _cartService.AddItemToCartAsync(sessionId, request.ProductId, request.Quantity);

            return Json(new
            {
                success = result.Success,
                message = result.Message,
                cartCount = await _cartService.GetCartCountAsync(sessionId)
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItems()
        {
            var sessionId = HttpContext.Session.Id;
            var cart = await _cartService.GetCartWithItemsAsync(sessionId);

            var items = cart.CartItems.Select(ci => new {
                id = ci.ProductId,
                name = ci.Product?.Name ?? "Unknown Product",
                price = ci.Product?.Price ?? 0,
                image = ci.Product?.ImageUrl ?? "/images/placeholder.png",
                quantity = ci.Quantity
            }).ToList();

            return Json(new { items });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateItem(int productId, int quantity)
        {
            var sessionId = HttpContext.Session.Id;
            await _cartService.UpdateCartItemAsync(sessionId, productId, quantity);
            return Json(new
            {
                success = true,
                cartCount = await _cartService.GetCartCountAsync(sessionId)
            });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            var sessionId = HttpContext.Session.Id;
            await _cartService.RemoveFromCartAsync(sessionId, productId);
            return Json(new
            {
                success = true,
                cartCount = await _cartService.GetCartCountAsync(sessionId)
            });
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var sessionId = HttpContext.Session.Id;
            var result = await _cartService.CheckoutAsync(sessionId);

            if (result.Success)
            {
                return RedirectToAction("Index", "OrderConfirmation");
            }

            return Json(new { success = false, message = result.Message });
        }
    }

    public class CartItemRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}