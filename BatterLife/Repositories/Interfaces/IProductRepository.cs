using BatterLife.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BatterLife.Repositories.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<Product> GetByIdAsync(int id);
        Task<Product> GetByIdWithDetailsAsync(int id);
        IQueryable<Product> GetAllWithDetailsAsync();
    }
}