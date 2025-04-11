using BatterLife.Models;
using BatterLife.Repositories.Interfaces;
using BatterLife.Services.Interfaces;

namespace BatterLife.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryWrapper _repository;

        public ProductService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await _repository.ProductRepository.GetAllWithDetailsAsync();

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