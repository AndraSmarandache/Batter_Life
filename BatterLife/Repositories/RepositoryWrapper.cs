using BatterLife.Models;
using BatterLife.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace BatterLife.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly BatterLifeDbContext _context;
        private ICartRepository _cartRepository;
        private IProductRepository _productRepository;

        public RepositoryWrapper(BatterLifeDbContext context)
        {
            _context = context;
        }

        public ICartRepository CartRepository =>
            _cartRepository ??= new CartRepository(_context);

        public IProductRepository ProductRepository =>
            _productRepository ??= new ProductRepository(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return await _context.Database.BeginTransactionAsync(isolationLevel);
        }
    }
}