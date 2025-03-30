using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace BatterLife.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }  // Stocare corectă în baza de date (20.00)
        public string ImageUrl { get; set; } = string.Empty;
        public List<string> Ingredients { get; set; } = new List<string>();
        public List<string> Allergens { get; set; } = new List<string>();
        public int CategoryId { get; set; }
        public ProductCategory? Category { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();

        [NotMapped]
        public double Rating { get; set; }
        [NotMapped]
        public string FormattedPrice => Price.ToString("0.00", CultureInfo.InvariantCulture) + " RON";
    }
}