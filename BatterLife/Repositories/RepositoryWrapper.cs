using BatterLife.Models;
using BatterLife.Repositories.Interfaces;

namespace BatterLife.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private BatterLifeDbContext _context;
        private IProductRepository? _productRepository;
        private ICartRepository? _cartRepository;

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_context);
                }
                return _productRepository;
            }
        }

        public ICartRepository CartRepository
        {
            get
            {
                if (_cartRepository == null)
                {
                    _cartRepository = new CartRepository(_context);
                }
                return _cartRepository;
            }
        }

        public RepositoryWrapper(BatterLifeDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}