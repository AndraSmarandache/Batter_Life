using BatterLife.Models;
using BatterLife.Repositories.Interfaces;
using BatterLife.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BatterLife.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryWrapper _repository;

        public ProductService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(string? category = null, string? sortBy = null)
        {
            var query = _repository.ProductRepository.GetAllWithDetailsAsync();

            if (!string.IsNullOrEmpty(category) && category != "all")
            {
                query = query.Where(p => p.Category.Name.ToLower() == category.ToLower());
            }

            query = sortBy switch
            {
                "name-asc" => query.OrderBy(p => p.Name),
                "name-desc" => query.OrderByDescending(p => p.Name),
                "price-asc" => query.OrderBy(p => p.Price),
                "price-desc" => query.OrderByDescending(p => p.Price),
                _ => query.OrderBy(p => p.Name),
            };

            var products = await query.ToListAsync();

            foreach (var product in products)
            {
                product.Rating = product.Reviews.Any() ?
                    product.Reviews.Average(r => r.Rating) : 0;
            }

            return products;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var product = await _repository.ProductRepository.GetByIdWithDetailsAsync(id);

            if (product != null)
            {
                product.Rating = product.Reviews.Any() ?
                    product.Reviews.Average(r => r.Rating) : 0;
            }

            return product;
        }
    }
}