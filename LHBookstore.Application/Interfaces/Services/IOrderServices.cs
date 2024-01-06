using LHBookstore.Application.DTOs.Order;
using LHBookstore.Domain;

namespace LHBookstore.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<ApiResponse<List<OrderResponseDto>>> GetAllOrdersAsync();
        Task<ApiResponse<OrderResponseDto>> GetOrderByIdAsync(string id);
        Task<ApiResponse<string>> PlaceOrderAsync(OrderRequestDto orderRequest);
        Task<ApiResponse<string>> UpdateOrderAsync(string id, OrderRequestDto orderRequest);
        Task<ApiResponse<string>> CancelOrderAsync(string id);
    }
}
