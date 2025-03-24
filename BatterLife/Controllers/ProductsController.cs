using Microsoft.AspNetCore.Mvc;
using BatterLife.Models;
using System.Collections.Generic;

namespace BatterLife.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Chocolate Cake", Category = "cakes", Price = 25, ImageUrl = "~/images/chocolate-cake.jpg", Ingredients = new List<string> { "Chocolate", "Flour", "Sugar", "Eggs" } },
                new Product { Id = 2, Name = "Strawberry Tart", Category = "pastries", Price = 15, ImageUrl = "~/images/strawberry-tart.jpg", Ingredients = new List<string> { "Strawberries", "Cream", "Pastry" } },
                new Product { Id = 3, Name = "Vanilla Cupcake", Category = "cakes", Price = 5, ImageUrl = "~/images/vanilla-cupcake.jpg", Ingredients = new List<string> { "Vanilla", "Flour", "Sugar", "Eggs" } },
                new Product { Id = 4, Name = "Almond Cookies", Category = "cookies", Price = 10, ImageUrl = "~/images/almond-cookies.jpg", Ingredients = new List<string> { "Almonds", "Flour", "Sugar", "Butter" } },
                new Product { Id = 5, Name = "Cheesecake", Category = "cakes", Price = 20, ImageUrl = "~/images/cheesecake.jpg", Ingredients = new List<string> { "Cream Cheese", "Biscuits", "Sugar", "Berries" } },
                new Product { Id = 6, Name = "Macarons", Category = "pastries", Price = 12, ImageUrl = "~/images/macarons.jpg", Ingredients = new List<string> { "Almond Flour", "Sugar", "Egg Whites", "Food Coloring" } },
                new Product { Id = 7, Name = "Brownies", Category = "cookies", Price = 8, ImageUrl = "~/images/brownies.jpg", Ingredients = new List<string> { "Chocolate", "Butter", "Sugar", "Flour" } },
                new Product { Id = 8, Name = "Tiramisu", Category = "desserts", Price = 18, ImageUrl = "~/images/tiramisu.jpg", Ingredients = new List<string> { "Mascarpone", "Coffee", "Ladyfingers", "Cocoa" } },
            };

            return View(products);
        }
    }
}