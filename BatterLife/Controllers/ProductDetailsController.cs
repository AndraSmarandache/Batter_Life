using BatterLife.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BatterLife.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly IProductDetailsService _productDetailsService;
        private readonly ICartService _cartService;

        public ProductDetailsController(
            IProductDetailsService productDetailsService,
            ICartService cartService)
        {
            _productDetailsService = productDetailsService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var product = await _productDetailsService.GetProductWithDetailsAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var product = await _productDetailsService.GetProductWithDetailsAsync(productId);
            if (product == null)
            {
                return Json(new { success = false, message = "Product not found" });
            }

            var sessionId = HttpContext.Session.Id;
            var result = await _cartService.AddItemToCartAsync(sessionId, productId, quantity);

            return Json(new
            {
                success = result.Success,
                message = result.Message,
                product = new
                {
                    id = product.Id,
                    name = product.Name,
                    price = product.Price,
                    imageUrl = product.ImageUrl,
                    formattedPrice = product.FormattedPrice
                },
                quantity = quantity
            });
        }
    }
}