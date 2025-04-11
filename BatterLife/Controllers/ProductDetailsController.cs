using BatterLife.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BatterLife.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly BatterLifeDbContext _context;

        public ProductDetailsController(BatterLifeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            product.Rating = product.Reviews.Any() ? product.Reviews.Average(r => r.Rating) : 0;

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return Json(new { success = false, message = "Product not found" });
            }

            return Json(new
            {
                success = true,
                product = new
                {
                    id = product.Id,
                    name = product.Name,
                    price = product.Price,
                    imageUrl = product.ImageUrl,
                    formattedPrice = product.FormattedPrice
                },
                quantity = quantity,
                message = $"{quantity} {product.Name}(s) added to cart"
            });
        }
    }
}