using BatterLife.Models;
using System.Threading.Tasks;

namespace BatterLife.Repositories.Interfaces
{
    public interface ICartRepository : IRepositoryBase<Cart>
    {
        Task<Cart> GetCartWithItemsAsync(string sessionId);
        Task AddItemToCartAsync(string sessionId, int productId, int quantity);
    }
}