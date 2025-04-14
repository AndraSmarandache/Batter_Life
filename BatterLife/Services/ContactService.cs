using System.Collections.Generic;
using System.Threading.Tasks;
using BatterLife.Models;
using BatterLife.Repositories.Interfaces;
using BatterLife.Services.Interfaces;

namespace BatterLife.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task AddMessageAsync(ContactModel contact)
        {
            await _contactRepository.AddAsync(contact);
        }

        public async Task<IEnumerable<ContactModel>> GetAllMessagesAsync()
        {
            return await _contactRepository.GetAllAsync();
        }
    }
}