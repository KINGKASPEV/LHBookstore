namespace LHBookstore.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository BookRepository { get; }
        IOrderRepository OrderRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
