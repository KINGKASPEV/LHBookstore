using LHBookstore.Domain.Entities;
using System.Linq.Expressions;

namespace LHBookstore.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task AddOrderAsync(Order order);
        Task DeleteOrderAsync(Order order);
        Task<List<Order>> FindOrdersAsync(Expression<Func<Order, bool>> condition);
        Task<Order> GetOrderByIdAsync(string id);
        Task UpdateOrderAsync(Order order);
    }
}