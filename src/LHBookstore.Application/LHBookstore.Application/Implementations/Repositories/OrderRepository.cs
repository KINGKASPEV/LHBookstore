using LHBookstore.Application.Interfaces.IRepositories;
using LHBookstore.Domain.Entities;
using LHBookstore.Infrastructure;
using System.Linq.Expressions;

namespace LHBookstore.Application.Implementations.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(LHBContext dbContext) : base(dbContext) { }
        public void AddBook(Order order) => Add(order);
        public void DeleteBook(Order order) => Delete(order);
        public List<Order> FindBook(Expression<Func<Order, bool>> condition) => FindByCondition(condition);
        public Order GetBookById(string id) => GetById(id);
        public List<Order> GetBooks() => GetAll();
        public void UpdateBook(Order order) => Update(order);

    }
}
