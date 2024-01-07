using LHBookstore.Application.DTOs.Order;
using LHBookstore.Application.Utilities;
using LHBookstore.Domain;

namespace LHBookstore.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<ApiResponse<PageResult<List<OrderResponseDto>>>> GetAllOrdersAsync(int page, int perPage);
        Task<ApiResponse<OrderResponseDto>> GetOrderByIdAsync(string id);
        Task<ApiResponse<string>> PlaceOrderAsync(OrderRequestDto orderRequest, string bookId);
        //Task<ApiResponse<string>> PlaceOrderAsync(OrderRequestDto orderRequest);
        Task<ApiResponse<string>> UpdateOrderAsync(string id, OrderRequestDto orderRequest);
        Task<ApiResponse<string>> CancelOrderAsync(string id);
    }
}
