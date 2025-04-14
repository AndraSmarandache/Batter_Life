using System.Collections.Generic;
using System.Threading.Tasks;
using BatterLife.Models;

namespace BatterLife.Repositories.Interfaces
{
    public interface IContactRepository
    {
        Task AddAsync(ContactModel contact);
        Task<IEnumerable<ContactModel>> GetAllAsync();
    }
}