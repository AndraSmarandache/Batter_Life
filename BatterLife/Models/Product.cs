using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BatterLife.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public List<string> Ingredients { get; set; } = new List<string>();
        public List<string> Allergens { get; set; } = new List<string>();
        public int CategoryId { get; set; }
        public ProductCategory? Category { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();

        [NotMapped]
        public double Rating { get; set; }
    }
}