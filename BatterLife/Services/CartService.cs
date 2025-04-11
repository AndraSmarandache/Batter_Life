using BatterLife.Models;
using BatterLife.Repositories.Interfaces;
using BatterLife.Services.Interfaces;
using System.Threading.Tasks;

namespace BatterLife.Services
{
    public class CartService : ICartService
    {
        private readonly IRepositoryWrapper _repository;

        public CartService(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<Cart> GetOrCreateCartAsync(string sessionId)
        {
            var cart = await _repository.CartRepository.GetBySessionIdAsync(sessionId);
            if (cart == null)
            {
                cart = new Cart { SessionId = sessionId };
                _repository.CartRepository.Create(cart);
                await _repository.SaveAsync();
            }
            return cart;
        }

        public async Task AddToCartAsync(string sessionId, int productId, int quantity)
        {
            var cart = await GetOrCreateCartAsync(sessionId);
            var existingItem = cart.CartItems.Find(ci => ci.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity
                });
            }

            await _repository.SaveAsync();
        }

        public async Task UpdateCartItemAsync(string sessionId, int productId, int quantity)
        {
            var cart = await GetOrCreateCartAsync(sessionId);
            var item = cart.CartItems.Find(ci => ci.ProductId == productId);

            if (item != null)
            {
                item.Quantity = quantity;
                await _repository.SaveAsync();
            }
        }

        public async Task RemoveFromCartAsync(string sessionId, int productId)
        {
            var cart = await GetOrCreateCartAsync(sessionId);
            var item = cart.CartItems.Find(ci => ci.ProductId == productId);

            if (item != null)
            {
                cart.CartItems.Remove(item);
                await _repository.SaveAsync();
            }
        }

        public async Task ClearCartAsync(string sessionId)
        {
            var cart = await GetOrCreateCartAsync(sessionId);
            cart.CartItems.Clear();
            await _repository.SaveAsync();
        }
    }
}