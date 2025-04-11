namespace BatterLife.Repositories.Interfaces
{
    public interface IRepositoryWrapper
    {
        IProductRepository ProductRepository { get; }
        ICartRepository CartRepository { get; }
        Task SaveAsync();
    }
}