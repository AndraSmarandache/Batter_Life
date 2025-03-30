using Microsoft.AspNetCore.Mvc;
using BatterLife.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BatterLife.Controllers
{
    public class ProductsController : Controller
    {
        private readonly BatterLifeDbContext _context;

        public ProductsController(BatterLifeDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .ToList();

            foreach (var product in products)
            {
                product.Rating = product.Reviews.Any() ?
                    product.Reviews.Average(r => r.Rating) : 0;
            }

            return View(products);
        }
    }
}