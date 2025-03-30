using BatterLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BatterLife.Utilities
{
    public static class DbInitializer
    {
        public static void Initialize(BatterLifeDbContext context)
        {
            context.Database.EnsureCreated();

            // Seed Categories
            if (!context.ProductCategories.Any())
            {
                var categories = new ProductCategory[]
                {
                    new ProductCategory{Name="Cakes", Description="Delicious cakes for all occasions"},
                    new ProductCategory{Name="Pastries", Description="Flaky and buttery pastries"},
                    new ProductCategory{Name="Cookies", Description="Sweet and crunchy cookies"},
                    new ProductCategory{Name="Desserts", Description="Decadent desserts to satisfy your sweet tooth"}
                };

                context.ProductCategories.AddRange(categories);
                context.SaveChanges();
            }

            // Seed Products with Reviews
            if (!context.Products.Any())
            {
                var cakeCategory = context.ProductCategories.First(c => c.Name == "Cakes");
                var pastryCategory = context.ProductCategories.First(c => c.Name == "Pastries");
                var cookieCategory = context.ProductCategories.First(c => c.Name == "Cookies");
                var dessertCategory = context.ProductCategories.First(c => c.Name == "Desserts");

                var products = new List<Product>
                {
                    new Product
                    {
                        Name = "Chocolate Cake",
                        CategoryId = cakeCategory.Id,
                        Price = 25.99M,
                        ImageUrl = "/images/chocolate-cake.jpg",
                        Description = "A rich and decadent chocolate cake, perfect for any occasion.",
                        Ingredients = new List<string> {"Chocolate", "Flour", "Sugar", "Eggs", "Butter", "Milk"},
                        Allergens = new List<string> {"Gluten", "Eggs", "Milk"},
                        Reviews = new List<Review>
                        {
                            new Review { UserName = "John Doe", Comment = "Amazing cake!", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-10) },
                            new Review { UserName = "Jane Smith", Comment = "Very tasty, but a bit too sweet.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-5) }
                        }
                    },
                    new Product
                    {
                        Name = "Strawberry Tart",
                        CategoryId = pastryCategory.Id,
                        Price = 15.00M,
                        ImageUrl = "/images/strawberry-tart.jpg",
                        Description = "A fresh and fruity strawberry tart with a buttery crust.",
                        Ingredients = new List<string> {"Strawberries", "Cream", "Pastry", "Sugar", "Butter"},
                        Allergens = new List<string> {"Gluten", "Dairy"},
                        Reviews = new List<Review>
                        {
                            new Review { UserName = "Alice Johnson", Comment = "Delicious and refreshing!", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-8) },
                            new Review { UserName = "Bob Brown", Comment = "The crust could be crispier.", Rating = 3, CreatedAt = DateTime.UtcNow.AddDays(-3) }
                        }
                    },
                    new Product
                    {
                        Name = "Vanilla Cupcake",
                        CategoryId = cakeCategory.Id,
                        Price = 5.00M,
                        ImageUrl = "/images/vanilla-cupcake.jpg",
                        Description = "A classic vanilla cupcake with creamy frosting.",
                        Ingredients = new List<string> {"Vanilla", "Flour", "Sugar", "Eggs", "Butter", "Milk"},
                        Allergens = new List<string> {"Gluten", "Eggs", "Milk"},
                        Reviews = new List<Review>
                        {
                            new Review { UserName = "Emily Davis", Comment = "Perfectly sweet and fluffy!", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-12) },
                            new Review { UserName = "Michael Wilson", Comment = "The frosting was a bit too sweet.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-7) }
                        }
                    },
                    new Product
                    {
                        Name = "Almond Cookies",
                        CategoryId = cookieCategory.Id,
                        Price = 10.00M,
                        ImageUrl = "/images/almond-cookies.jpg",
                        Description = "Crunchy almond cookies with a hint of vanilla.",
                        Ingredients = new List<string> {"Almonds", "Flour", "Sugar", "Butter", "Vanilla"},
                        Allergens = new List<string> {"Gluten", "Nuts"},
                        Reviews = new List<Review>
                        {
                            new Review { UserName = "Sarah Lee", Comment = "Loved the almond flavor!", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-9) },
                            new Review { UserName = "David Clark", Comment = "A bit too dry for my taste.", Rating = 3, CreatedAt = DateTime.UtcNow.AddDays(-4) }
                        }
                    },
                    new Product
                    {
                        Name = "Cheesecake",
                        CategoryId = cakeCategory.Id,
                        Price = 20.00M,
                        ImageUrl = "/images/cheesecake.jpg",
                        Description = "Creamy cheesecake with a biscuit base and berry topping.",
                        Ingredients = new List<string> {"Cream Cheese", "Biscuits", "Sugar", "Berries", "Butter"},
                        Allergens = new List<string> {"Gluten", "Dairy"},
                        Reviews = new List<Review>
                        {
                            new Review { UserName = "Laura Green", Comment = "The best cheesecake I've ever had!", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-15) },
                            new Review { UserName = "James White", Comment = "A bit too rich, but still delicious.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-10) }
                        }
                    },
                    new Product
                    {
                        Name = "Macarons",
                        CategoryId = pastryCategory.Id,
                        Price = 12.00M,
                        ImageUrl = "/images/macarons.jpg",
                        Description = "Colorful and delicate French macarons with various flavors.",
                        Ingredients = new List<string> {"Almond Flour", "Sugar", "Egg Whites", "Food Coloring"},
                        Allergens = new List<string> {"Nuts", "Eggs"},
                        Reviews = new List<Review>
                        {
                            new Review { UserName = "Olivia Harris", Comment = "So pretty and tasty!", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-11) },
                            new Review { UserName = "Daniel Martin", Comment = "A bit too sweet for me.", Rating = 3, CreatedAt = DateTime.UtcNow.AddDays(-6) }
                        }
                    },
                    new Product
                    {
                        Name = "Brownies",
                        CategoryId = cookieCategory.Id,
                        Price = 8.00M,
                        ImageUrl = "/images/brownies.jpg",
                        Description = "Fudgy and chocolatey brownies with a rich flavor.",
                        Ingredients = new List<string> {"Chocolate", "Butter", "Sugar", "Flour", "Eggs"},
                        Allergens = new List<string> {"Gluten", "Eggs"},
                        Reviews = new List<Review>
                        {
                            new Review { UserName = "Sophia Martinez", Comment = "Perfectly fudgy and delicious!", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-14) },
                            new Review { UserName = "William Anderson", Comment = "Could be more chocolatey.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-9) }
                        }
                    },
                    new Product
                    {
                        Name = "Tiramisu",
                        CategoryId = dessertCategory.Id,
                        Price = 18.00M,
                        ImageUrl = "/images/tiramisu.jpg",
                        Description = "Classic Italian tiramisu with coffee and mascarpone.",
                        Ingredients = new List<string> {"Mascarpone", "Coffee", "Ladyfingers", "Cocoa", "Sugar"},
                        Allergens = new List<string> {"Gluten", "Dairy"},
                        Reviews = new List<Review>
                        {
                            new Review { UserName = "Emma Taylor", Comment = "Absolutely divine!", Rating = 5, CreatedAt = DateTime.UtcNow.AddDays(-13) },
                            new Review { UserName = "Noah Thomas", Comment = "A bit too strong on the coffee flavor.", Rating = 4, CreatedAt = DateTime.UtcNow.AddDays(-8) }
                        }
                    }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}