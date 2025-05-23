﻿using BatterLife.Models;
using BatterLife.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BatterLife.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(BatterLifeDbContext context) : base(context) { }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await Context.Products.FindAsync(id);
        }

        public async Task<Product> GetByIdWithDetailsAsync(int id)
        {
            return await Context.Products
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public IQueryable<Product> GetAllWithDetailsAsync()
        {
            return Context.Products
                .Include(p => p.Category)
                .Include(p => p.Reviews);
        }
    }
}