using BatterLife.Models;
using BatterLife.Repositories.Interfaces;
using BatterLife.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
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

        public async Task<Cart> GetCartWithItemsAsync(string sessionId)
        {
            return await _repository.CartRepository.GetCartWithItemsAsync(sessionId);
        }

        public async Task<CartOperationResult> AddItemToCartAsync(string sessionId, int productId, int quantity)
        {
            using var transaction = await _repository.BeginTransactionAsync();

            try
            {
                var product = await _repository.ProductRepository.GetByIdAsync(productId);
                if (product == null)
                {
                    return new CartOperationResult
                    {
                        Success = false,
                        Message = "Product not found"
                    };
                }

                await _repository.CartRepository.AddItemToCartAsync(sessionId, productId, quantity);
                await _repository.SaveAsync();
                await transaction.CommitAsync();

                return new CartOperationResult
                {
                    Success = true,
                    Message = $"{product.Name} added to cart"
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new CartOperationResult
                {
                    Success = false,
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        public async Task UpdateCartItemAsync(string sessionId, int productId, int quantity)
        {
            using var transaction = await _repository.BeginTransactionAsync();

            try
            {
                var cart = await GetCartWithItemsAsync(sessionId);
                var item = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

                if (item != null)
                {
                    item.Quantity += quantity;
                    if (item.Quantity <= 0)
                    {
                        cart.CartItems.Remove(item);
                    }
                    await _repository.SaveAsync();
                    await transaction.CommitAsync();
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task RemoveFromCartAsync(string sessionId, int productId)
        {
            using var transaction = await _repository.BeginTransactionAsync();

            try
            {
                var cart = await GetCartWithItemsAsync(sessionId);
                var item = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

                if (item != null)
                {
                    cart.CartItems.Remove(item);
                    await _repository.SaveAsync();
                    await transaction.CommitAsync();
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<int> GetCartCountAsync(string sessionId)
        {
            var cart = await GetCartWithItemsAsync(sessionId);
            return cart.CartItems.Sum(ci => ci.Quantity);
        }

        public async Task<CartOperationResult> CheckoutAsync(string sessionId)
        {
            using var transaction = await _repository.BeginTransactionAsync();

            try
            {
                var cart = await GetCartWithItemsAsync(sessionId);
                if (!cart.CartItems.Any())
                {
                    return new CartOperationResult
                    {
                        Success = false,
                        Message = "Cart is empty"
                    };
                }

                cart.CartItems.Clear();
                await _repository.SaveAsync();
                await transaction.CommitAsync();

                return new CartOperationResult
                {
                    Success = true,
                    Message = "Checkout successful"
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new CartOperationResult
                {
                    Success = false,
                    Message = $"Checkout failed: {ex.Message}"
                };
            }
        }
    }
}