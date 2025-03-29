using Microsoft.AspNetCore.Mvc;
using BatterLife.Models;

namespace BatterLife.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Chocolate Cake", Category = "cakes", Price = 25, ImageUrl = "/images/chocolate-cake.jpg" },
                new Product { Id = 2, Name = "Strawberry Tart", Category = "pastries", Price = 15, ImageUrl = "/images/strawberry-tart.jpg" },
                new Product { Id = 3, Name = "Vanilla Cupcake", Category = "cakes", Price = 5, ImageUrl = "/images/vanilla-cupcake.jpg" },
                new Product { Id = 4, Name = "Almond Cookies", Category = "cookies", Price = 10, ImageUrl = "/images/almond-cookies.jpg" },
                new Product { Id = 5, Name = "Cheesecake", Category = "cakes", Price = 20, ImageUrl = "/images/cheesecake.jpg" },
                new Product { Id = 6, Name = "Macarons", Category = "pastries", Price = 12, ImageUrl = "/images/macarons.jpg" },
                new Product { Id = 7, Name = "Brownies", Category = "cookies", Price = 8, ImageUrl = "/images/brownies.jpg" },
                new Product { Id = 8, Name = "Tiramisu", Category = "desserts", Price = 18, ImageUrl = "/images/tiramisu.jpg" }
            };

            return View(products);
        }
    }
}