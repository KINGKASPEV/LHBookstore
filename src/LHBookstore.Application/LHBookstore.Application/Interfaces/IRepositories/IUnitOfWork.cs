namespace LHBookstore.Application.Interfaces.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository BookRepository { get; }
        IOrderRepository OrderRepository { get; }
        int SaveChanges();
    }
}
