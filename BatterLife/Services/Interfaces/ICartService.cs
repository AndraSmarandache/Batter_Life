using BatterLife.Models;
using System.Threading.Tasks;

namespace BatterLife.Services.Interfaces
{
    public interface ICartService
    {
        Task<Cart> GetOrCreateCartAsync(string sessionId);
        Task AddToCartAsync(string sessionId, int productId, int quantity);
        Task UpdateCartItemAsync(string sessionId, int productId, int quantity);
        Task RemoveFromCartAsync(string sessionId, int productId);
        Task ClearCartAsync(string sessionId);
    }
}