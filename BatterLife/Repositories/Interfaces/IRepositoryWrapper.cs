using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace BatterLife.Repositories.Interfaces
{
    public interface IRepositoryWrapper
    {
        ICartRepository CartRepository { get; }
        IProductRepository ProductRepository { get; }
        Task SaveAsync();
        Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    }
}