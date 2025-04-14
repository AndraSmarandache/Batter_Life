using BatterLife.Models;
using BatterLife.Repositories.Interfaces;
using BatterLife.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace BatterLife.Services
{
    public class ProductDetailsService : IProductDetailsService
    {
        private readonly IRepositoryWrapper _repository;

        public ProductDetailsService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<Product?> GetProductWithDetailsAsync(int id)
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