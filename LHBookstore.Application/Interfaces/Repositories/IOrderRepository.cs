using LHBookstore.Domain.Entities;
using System.Linq.Expressions;

namespace LHBookstore.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        void AddOrder(Order order);
        void DeleteOrder(Order order);
        public List<Order> FindOrders(Expression<Func<Order, bool>> condition);
        Order GetOrderById(string id);
        void UpdateOrder(Order order);
    }
}
