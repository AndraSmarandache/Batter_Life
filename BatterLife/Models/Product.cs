﻿using BatterLife.ViewModels;

namespace BatterLife.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Allergens { get; set; }
        public double Rating { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
