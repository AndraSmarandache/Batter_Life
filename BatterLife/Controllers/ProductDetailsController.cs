using Microsoft.AspNetCore.Mvc;
using BatterLife.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BatterLife.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly BatterLifeDbContext _context;

        public ProductDetailsController(BatterLifeDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            product.Rating = product.Reviews.Any() ?
                product.Reviews.Average(r => r.Rating) : 0;

            return View(product);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            var product = _context.Products.Find(productId);

            if (product == null)
            {
                return Json(new { success = false, message = "Product not found" });
            }

            return Json(new
            {
                success = true,
                message = $"Added {quantity} {product.Name}(s) to cart",
                totalPrice = product.Price * quantity
            });
        }

        [HttpGet]
        public IActionResult GetReviews(int productId)
        {
            var product = _context.Products
                .Include(p => p.Reviews)
                .FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                return Json(new { success = false, message = "Product not found" });
            }

            return Json(new
            {
                success = true,
                reviews = product.Reviews.Select(r => new {
                    UserName = r.UserName,
                    Comment = r.Comment,
                    Rating = r.Rating
                })
            });
        }
    }
}