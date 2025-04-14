using System.Collections.Generic;
using System.Threading.Tasks;
using BatterLife.Models;
using BatterLife.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BatterLife.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly BatterLifeDbContext _dbContext;

        public ContactRepository(BatterLifeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(ContactModel contact)
        {
            await _dbContext.ContactMessages.AddAsync(contact);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ContactModel>> GetAllAsync()
        {
            return await _dbContext.ContactMessages.ToListAsync();
        }
    }
}