using System.Collections.Generic;
using System.Threading.Tasks;
using BatterLife.Models;

namespace BatterLife.Services.Interfaces
{
    public interface IContactService
    {
        Task AddMessageAsync(ContactModel contact);
        Task<IEnumerable<ContactModel>> GetAllMessagesAsync();
    }
}