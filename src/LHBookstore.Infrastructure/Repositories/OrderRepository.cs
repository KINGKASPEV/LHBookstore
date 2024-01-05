using LHBookstore.Application.Interfaces.Repositories;
using LHBookstore.Domain.Entities;
using LHBookstore.Infrastructure;
using System.Linq.Expressions;

namespace LHBookstore.Application.Implementations.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(LHBContext dbContext) : base(dbContext) { }
        public void AddOrder(Order order) => Add(order);
        public void DeleteOrder(Order order) => Delete(order);
        public List<Order> FindOrders(Expression<Func<Order, bool>> condition) => FindByCondition(condition);
        public Order GetOrderById(string id) => GetById(id);
        public List<Order> GetAllOrders() => GetAll();
        public void UpdateOrder(Order order) => Update(order);
    }
}
