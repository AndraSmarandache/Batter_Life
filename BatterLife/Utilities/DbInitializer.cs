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

            if (!context.ProductCategories.Any())
            {
                var categories = new ProductCategory[]
                {
                    new ProductCategory{Name="Cakes", Description="Delicious cakes"},
                    new ProductCategory{Name="Pastries", Description="Flaky pastries"},
                    new ProductCategory{Name="Cookies", Description="Sweet cookies"},
                    new ProductCategory{Name="Desserts", Description="Tasty desserts"}
                };

                context.ProductCategories.AddRange(categories);
                context.SaveChanges();
            }

            if (!context.Products.Any())
            {
                var cakeCategory = context.ProductCategories.First(c => c.Name == "Cakes");
                var pastryCategory = context.ProductCategories.First(c => c.Name == "Pastries");

                var products = new Product[]
                {
                    new Product{
                        Name="Chocolate Cake",
                        CategoryId = cakeCategory.Id,
                        Price=25.99M,
                        Description="Rich chocolate cake",
                        ImageUrl="/images/chocolate-cake.jpg",
                        Ingredients=new List<string>{"Flour", "Sugar", "Cocoa"},
                        Allergens=new List<string>{"Gluten", "Dairy"},
                        Reviews = new List<Review> {
                            new Review { UserName = "John Doe", Comment = "Amazing cake!", Rating = 5 },
                            new Review { UserName = "Jane Smith", Comment = "Very tasty!", Rating = 4 }
                        }
                    },
                    new Product{
                        Name="Strawberry Tart",
                        CategoryId = pastryCategory.Id,
                        Price=18.50M,
                        Description="Fresh strawberry tart",
                        ImageUrl="/images/strawberry-tart.jpg",
                        Ingredients=new List<string>{"Strawberries", "Pastry"},
                        Allergens=new List<string>{"Gluten"},
                        Reviews = new List<Review> {
                            new Review { UserName = "Alice Johnson", Comment = "Delicious!", Rating = 5 },
                            new Review { UserName = "Bob Brown", Comment = "Great texture", Rating = 4 }
                        }
                    }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}