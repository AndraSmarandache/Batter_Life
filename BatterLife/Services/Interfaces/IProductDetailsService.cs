using BatterLife.Models;
using System.Threading.Tasks;

namespace BatterLife.Services.Interfaces
{
    public interface IProductDetailsService
    {
        Task<Product?> GetProductWithDetailsAsync(int id);
    }
}