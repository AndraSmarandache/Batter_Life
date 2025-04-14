using BatterLife.Models;
using BatterLife.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BatterLife.Repositories
{
    public class CartRepository : RepositoryBase<Cart>, ICartRepository
    {
        public CartRepository(BatterLifeDbContext context) : base(context) { }

        public async Task<Cart> GetCartWithItemsAsync(string sessionId)
        {
            return await Context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.SessionId == sessionId)
                ?? new Cart { SessionId = sessionId };
        }

        public async Task AddItemToCartAsync(string sessionId, int productId, int quantity)
        {
            var cart = await GetCartWithItemsAsync(sessionId);

            if (cart.Id == 0) 
            {
                Context.Carts.Add(cart);
            }

            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

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
        }
    }
}