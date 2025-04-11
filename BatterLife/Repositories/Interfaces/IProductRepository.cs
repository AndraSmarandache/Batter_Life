using BatterLife.Models;

namespace BatterLife.Repositories.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<IEnumerable<Product>> GetAllWithDetailsAsync();
        Task<Product?> GetByIdWithDetailsAsync(int id);
    }
}