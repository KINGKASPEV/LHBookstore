using LHBookstore.Application.Interfaces.Repositories;
using LHBookstore.Application.Interfaces.Services;
using LHBookstore.Domain.Entities;

namespace LHBookstore.Application.ServiceImplementations
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _unitOfWork.OrderRepository.GetAllOrdersAsync();
        }

        public async Task<Order> GetOrderByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id), "Order id cannot be null or empty.");
            }

            return await _unitOfWork.OrderRepository.GetOrderByIdAsync(id);
        }

        public async Task PlaceOrderAsync(Order order)
        {
            // Additional logic related to placing an order
            await _unitOfWork.OrderRepository.AddOrderAsync(order);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            // Additional logic related to updating an order
            var existingOrder = await _unitOfWork.OrderRepository.GetOrderByIdAsync(order.Id);

            if (existingOrder == null)
            {
                throw new InvalidOperationException($"Order with id {order.Id} not found.");
            }

            _unitOfWork.OrderRepository.UpdateOrderAsync(order);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CancelOrderAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id), "Order id cannot be null or empty.");
            }

            var order = await _unitOfWork.OrderRepository.GetOrderByIdAsync(id);

            if (order != null)
            {
                // Additional logic related to canceling an order
                _unitOfWork.OrderRepository.DeleteOrderAsync(order);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
