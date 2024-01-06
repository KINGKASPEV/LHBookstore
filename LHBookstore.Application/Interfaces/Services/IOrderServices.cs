using LHBookstore.Domain.Entities;

namespace LHBookstore.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(string id);
        Task PlaceOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task CancelOrderAsync(string id);
    }
}
