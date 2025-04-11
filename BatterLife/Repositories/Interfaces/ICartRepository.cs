using BatterLife.Models;

namespace BatterLife.Repositories.Interfaces
{
    public interface ICartRepository : IRepositoryBase<Cart>
    {
        Task<Cart?> GetBySessionIdAsync(string sessionId);
        Task AddItemToCartAsync(string sessionId, int productId, int quantity);
    }
}