using LHBookstore.Domain.Entities;
using System.Linq.Expressions;

namespace LHBookstore.Application.Interfaces.IRepositories
{
    public interface IOrderRepository
    {
        List<Order> GetBooks();
        void AddBook(Order order);
        void DeleteBook(Order order);
        public List<Order> FindBook(Expression<Func<Order, bool>> condition);
        Order GetBookById(string id);
        void UpdateBook(Order order);
    }
}
