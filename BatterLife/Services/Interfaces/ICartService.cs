using BatterLife.Models;
using System.Threading.Tasks;

namespace BatterLife.Services.Interfaces
{
    public interface ICartService
    {
        Task<Cart> GetCartWithItemsAsync(string sessionId);
        Task<CartOperationResult> AddItemToCartAsync(string sessionId, int productId, int quantity);
        Task UpdateCartItemAsync(string sessionId, int productId, int quantity);
        Task RemoveFromCartAsync(string sessionId, int productId);
        Task<int> GetCartCountAsync(string sessionId);
        Task<CartOperationResult> CheckoutAsync(string sessionId);
    }

    public class CartOperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}