﻿using LHBookstore.Application.DTOs.Order;
using LHBookstore.Application.Utilities;
using LHBookstore.Domain;

namespace LHBookstore.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<ApiResponse<PageResult<List<OrderResponseDto>>>> GetAllOrdersAsync(int page, int perPage);
        Task<ApiResponse<OrderResponseDto>> GetOrderByIdAsync(string id);
        Task<ApiResponse<OrderResponseDto>> PlaceOrderAsync(OrderRequestDto orderRequest, string bookId);
        Task<ApiResponse<OrderResponseDto>> UpdateOrderAsync(string id, OrderRequestDto orderRequest);
        Task<ApiResponse<PageResult<List<OrderResponseDto>>>> GetOrdersByStatusAsync(string status, int page, int perPage);
        Task<ApiResponse<string>> CancelOrderAsync(string id);
    }
}
