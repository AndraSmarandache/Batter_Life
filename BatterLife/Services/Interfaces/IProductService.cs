// BatterLife.Services.Interfaces/IProductService.cs
using BatterLife.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatterLife.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(string? category = null, string? sortBy = null);
        Task<Product?> GetProductByIdAsync(int id);
    }
}