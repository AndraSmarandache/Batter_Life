using Microsoft.AspNetCore.Mvc;
using BatterLife.Models;
using BatterLife.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace BatterLife.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly List<Product> _products; 

        public ProductDetailsController()
        {
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Chocolate Cake", Category = "cakes", Price = 25, ImageUrl = "~/images/chocolate-cake.jpg", Ingredients = new List<string> { "Chocolate", "Flour", "Sugar", "Eggs" }, Description = "A delicious chocolate cake." },
                new Product { Id = 2, Name = "Strawberry Tart", Category = "pastries", Price = 15, ImageUrl = "~/images/strawberry-tart.jpg", Ingredients = new List<string> { "Strawberries", "Cream", "Pastry" }, Description = "A sweet strawberry tart." },
                new Product { Id = 3, Name = "Vanilla Cupcake", Category = "cakes", Price = 5, ImageUrl = "~/images/vanilla-cupcake.jpg", Ingredients = new List<string> { "Vanilla", "Flour", "Sugar", "Eggs" }, Description = "A classic vanilla cupcake." },
                new Product { Id = 4, Name = "Almond Cookies", Category = "cookies", Price = 10, ImageUrl = "~/images/almond-cookies.jpg", Ingredients = new List<string> { "Almonds", "Flour", "Sugar", "Butter" }, Description = "Crunchy almond cookies." },
                new Product { Id = 5, Name = "Cheesecake", Category = "cakes", Price = 20, ImageUrl = "~/images/cheesecake.jpg", Ingredients = new List<string> { "Cream Cheese", "Biscuits", "Sugar", "Berries" }, Description = "Rich and creamy cheesecake." },
                new Product { Id = 6, Name = "Macarons", Category = "pastries", Price = 12, ImageUrl = "~/images/macarons.jpg", Ingredients = new List<string> { "Almond Flour", "Sugar", "Egg Whites", "Food Coloring" }, Description = "Colorful macarons with various flavors." },
                new Product { Id = 7, Name = "Brownies", Category = "cookies", Price = 8, ImageUrl = "~/images/brownies.jpg", Ingredients = new List<string> { "Chocolate", "Butter", "Sugar", "Flour" }, Description = "Chewy and chocolatey brownies." },
                new Product { Id = 8, Name = "Tiramisu", Category = "desserts", Price = 18, ImageUrl = "~/images/tiramisu.jpg", Ingredients = new List<string> { "Mascarpone", "Coffee", "Ladyfingers", "Cocoa" }, Description = "Italian tiramisu with coffee flavor." },

            };
        }

        public IActionResult Index(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(); 
            }

            
            var reviews = new List<Review>
            {
                new Review { Id = 1, ProductId = id, UserName = "User1", Rating = 4, Comment = "Great product!" },
                new Review { Id = 2, ProductId = id, UserName = "User2", Rating = 5, Comment = "Loved it!" },
                new Review { Id = 3, ProductId = id, UserName = "User3", Rating = 3, Comment = "It was okay." },
                new Review { Id = 4, ProductId = id, UserName = "User4", Rating = 5, Comment = "Best cake ever!" },
                new Review { Id = 5, ProductId = id, UserName = "User5", Rating = 4, Comment = "Very tasty." },

            };

            var viewModel = new ProductDetailsViewModel
            {
                Product = product,
                Reviews = reviews
            };

            return View(viewModel);
        }
    }
}