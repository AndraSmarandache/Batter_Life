using Microsoft.AspNetCore.Mvc;
using BatterLife.Models;
using System.Collections.Generic;
using System.Linq;

namespace BatterLife.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly List<Product> _products = new List<Product>
        {
            new Product {
                Id = 1,
                Name = "Chocolate Cake",
                Category = "cakes",
                Price = 25,
                ImageUrl = "/images/chocolate-cake.jpg",
                Description = "A rich and decadent chocolate cake, perfect for any occasion.",
                Ingredients = new List<string> { "Chocolate", "Flour", "Sugar", "Eggs", "Butter", "Milk" },
                Allergens = new List<string> { "Gluten", "Eggs", "Milk" },
                Rating = 4.5,
                Reviews = new List<Review> {
                    new Review { Id = 1, User = "John Doe", Comment = "Amazing cake!", Rating = 5 },
                    new Review { Id = 2, User = "Jane Smith", Comment = "Very tasty, but a bit too sweet.", Rating = 4 }
                }
            },
            new Product {
                Id = 2,
                Name = "Strawberry Tart",
                Category = "pastries",
                Price = 15,
                ImageUrl = "/images/strawberry-tart.jpg",
                Description = "A fresh and fruity strawberry tart with a buttery crust.",
                Ingredients = new List<string> { "Strawberries", "Cream", "Pastry", "Sugar", "Butter" },
                Allergens = new List<string> { "Gluten", "Dairy" },
                Rating = 4.2,
                Reviews = new List<Review> {
                    new Review { Id = 1, User = "Alice Johnson", Comment = "Delicious and refreshing!", Rating = 5 },
                    new Review { Id = 2, User = "Bob Brown", Comment = "The crust could be crispier.", Rating = 3 }
                }
            },
            new Product {
                Id = 3,
                Name = "Vanilla Cupcake",
                Category = "cakes",
                Price = 5,
                ImageUrl = "/images/vanilla-cupcake.jpg",
                Description = "A classic vanilla cupcake with creamy frosting.",
                Ingredients = new List<string> { "Vanilla", "Flour", "Sugar", "Eggs", "Butter", "Milk" },
                Allergens = new List<string> { "Gluten", "Eggs", "Milk" },
                Rating = 4.7,
                Reviews = new List<Review> {
                    new Review { Id = 1, User = "Emily Davis", Comment = "Perfectly sweet and fluffy!", Rating = 5 },
                    new Review { Id = 2, User = "Michael Wilson", Comment = "The frosting was a bit too sweet.", Rating = 4 }
                }
            },
            new Product {
                Id = 4,
                Name = "Almond Cookies",
                Category = "cookies",
                Price = 10,
                ImageUrl = "/images/almond-cookies.jpg",
                Description = "Crunchy almond cookies with a hint of vanilla.",
                Ingredients = new List<string> { "Almonds", "Flour", "Sugar", "Butter", "Vanilla" },
                Allergens = new List<string> { "Gluten", "Nuts" },
                Rating = 4.0,
                Reviews = new List<Review> {
                    new Review { Id = 1, User = "Sarah Lee", Comment = "Loved the almond flavor!", Rating = 4 },
                    new Review { Id = 2, User = "David Clark", Comment = "A bit too dry for my taste.", Rating = 3 }
                }
            },
            new Product {
                Id = 5,
                Name = "Cheesecake",
                Category = "cakes",
                Price = 20,
                ImageUrl = "/images/cheesecake.jpg",
                Description = "Creamy cheesecake with a biscuit base and berry topping.",
                Ingredients = new List<string> { "Cream Cheese", "Biscuits", "Sugar", "Berries", "Butter" },
                Allergens = new List<string> { "Gluten", "Dairy" },
                Rating = 4.8,
                Reviews = new List<Review> {
                    new Review { Id = 1, User = "Laura Green", Comment = "The best cheesecake I've ever had!", Rating = 5 },
                    new Review { Id = 2, User = "James White", Comment = "A bit too rich, but still delicious.", Rating = 4 }
                }
            },
            new Product {
                Id = 6,
                Name = "Macarons",
                Category = "pastries",
                Price = 12,
                ImageUrl = "/images/macarons.jpg",
                Description = "Colorful and delicate French macarons with various flavors.",
                Ingredients = new List<string> { "Almond Flour", "Sugar", "Egg Whites", "Food Coloring" },
                Allergens = new List<string> { "Nuts", "Eggs" },
                Rating = 4.6,
                Reviews = new List<Review> {
                    new Review { Id = 1, User = "Olivia Harris", Comment = "So pretty and tasty!", Rating = 5 },
                    new Review { Id = 2, User = "Daniel Martin", Comment = "A bit too sweet for me.", Rating = 3 }
                }
            },
            new Product {
                Id = 7,
                Name = "Brownies",
                Category = "cookies",
                Price = 8,
                ImageUrl = "/images/brownies.jpg",
                Description = "Fudgy and chocolatey brownies with a rich flavor.",
                Ingredients = new List<string> { "Chocolate", "Butter", "Sugar", "Flour", "Eggs" },
                Allergens = new List<string> { "Gluten", "Eggs" },
                Rating = 4.3,
                Reviews = new List<Review> {
                    new Review { Id = 1, User = "Sophia Martinez", Comment = "Perfectly fudgy and delicious!", Rating = 5 },
                    new Review { Id = 2, User = "William Anderson", Comment = "Could be more chocolatey.", Rating = 4 }
                }
            },
            new Product {
                Id = 8,
                Name = "Tiramisu",
                Category = "desserts",
                Price = 18,
                ImageUrl = "/images/tiramisu.jpg",
                Description = "Classic Italian tiramisu with coffee and mascarpone.",
                Ingredients = new List<string> { "Mascarpone", "Coffee", "Ladyfingers", "Cocoa", "Sugar" },
                Allergens = new List<string> { "Gluten", "Dairy" },
                Rating = 4.9,
                Reviews = new List<Review> {
                    new Review { Id = 1, User = "Emma Taylor", Comment = "Absolutely divine!", Rating = 5 },
                    new Review { Id = 2, User = "Noah Thomas", Comment = "A bit too strong on the coffee flavor.", Rating = 4 }
                }
            }
        };

        public IActionResult Index(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            var product = _products.FirstOrDefault(p => p.Id == productId);

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
            var product = _products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                return Json(new { success = false, message = "Product not found" });
            }

            return Json(new
            {
                success = true,
                reviews = product.Reviews
            });
        }
    }
}