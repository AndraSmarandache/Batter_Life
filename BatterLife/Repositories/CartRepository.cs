using BatterLife.Models;
using BatterLife.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BatterLife.Repositories
{
    public class CartRepository : RepositoryBase<Cart>, ICartRepository
    {
        public CartRepository(BatterLifeDbContext context) : base(context) { }

        public async Task<Cart?> GetBySessionIdAsync(string sessionId)
        {
            return await Context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.SessionId == sessionId);
        }

        public async Task AddItemToCartAsync(string sessionId, int productId, int quantity)
        {
            var cart = await GetBySessionIdAsync(sessionId);
            var product = await Context.Products.FindAsync(productId);

            if (product == null)
            {
                throw new ArgumentException("Product not found", nameof(productId));
            }

            if (cart == null)
            {
                cart = new Cart
                {
                    SessionId = sessionId,
                    CartItems = new List<CartItem>()
                };
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

            await Context.SaveChangesAsync();
        }
    }
}