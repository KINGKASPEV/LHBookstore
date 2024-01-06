using AutoMapper;
using LHBookstore.Application.DTOs.Order;
using LHBookstore.Application.Interfaces.Repositories;
using LHBookstore.Application.Interfaces.Services;
using LHBookstore.Domain;
using LHBookstore.Domain.Entities;

namespace LHBookstore.Application.ServiceImplementations
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<OrderResponseDto>>> GetAllOrdersAsync()
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetAllOrdersAsync();
                var orderDtos = _mapper.Map<List<OrderResponseDto>>(orders);

                return ApiResponse<List<OrderResponseDto>>.Success(orderDtos, "Orders retrieved successfully", 200);
            }
            catch (Exception ex)
            {
                // Log the exception for further investigation
                Console.WriteLine($"Error retrieving orders: {ex.Message}");

                return ApiResponse<List<OrderResponseDto>>.Failed(false, "An error occurred while retrieving orders", 500, new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponse<OrderResponseDto>> GetOrderByIdAsync(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return ApiResponse<OrderResponseDto>.Failed(false, "Order id cannot be null or empty", 400, null);
                }

                var order = await _unitOfWork.OrderRepository.GetOrderByIdAsync(id);

                if (order == null)
                {
                    return ApiResponse<OrderResponseDto>.Failed(false, $"Order with id {id} not found", 404, null);
                }

                var orderDto = _mapper.Map<OrderResponseDto>(order);

                return ApiResponse<OrderResponseDto>.Success(orderDto, "Order retrieved successfully", 200);
            }
            catch (Exception ex)
            {
                // Log the exception for further investigation
                Console.WriteLine($"Error retrieving order: {ex.Message}");

                return ApiResponse<OrderResponseDto>.Failed(false, "An error occurred while retrieving the order", 500, new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponse<string>> PlaceOrderAsync(OrderRequestDto orderRequest)
        {
            try
            {
                if (orderRequest == null || orderRequest.OrderItems == null || orderRequest.OrderItems.Count == 0)
                {
                    return ApiResponse<string>.Failed(false, "Order request is null or empty", 400, null);
                }

                // Additional logic related to placing an order

                var order = _mapper.Map<Order>(orderRequest);
                await _unitOfWork.OrderRepository.AddOrderAsync(order);
                await _unitOfWork.SaveChangesAsync();

                return ApiResponse<string>.Success(order.Id, "Order placed successfully", 201);
            }
            catch (Exception ex)
            {
                // Log the exception for further investigation
                Console.WriteLine($"Error placing order: {ex.Message}");

                return ApiResponse<string>.Failed(false, "An error occurred while placing the order", 500, new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponse<string>> UpdateOrderAsync(string id, OrderRequestDto orderRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return ApiResponse<string>.Failed(false, "Order id cannot be null or empty", 400, null);
                }

                var existingOrder = await _unitOfWork.OrderRepository.GetOrderByIdAsync(id);

                if (existingOrder == null)
                {
                    return ApiResponse<string>.Failed(false, $"Order with id {id} not found", 404, null);
                }

                // Additional logic related to updating an order

                _mapper.Map(orderRequest, existingOrder);

                _unitOfWork.OrderRepository.UpdateOrderAsync(existingOrder);
                await _unitOfWork.SaveChangesAsync();

                return ApiResponse<string>.Success(existingOrder.Id, "Order updated successfully", 200);
            }
            catch (Exception ex)
            {
                // Log the exception for further investigation
                Console.WriteLine($"Error updating order: {ex.Message}");

                return ApiResponse<string>.Failed(false, "An error occurred while updating the order", 500, new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponse<string>> CancelOrderAsync(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return ApiResponse<string>.Failed(false, "Order id cannot be null or empty", 400, null);
                }

                var order = await _unitOfWork.OrderRepository.GetOrderByIdAsync(id);

                if (order == null)
                {
                    return ApiResponse<string>.Failed(false, $"Order with id {id} not found", 404, null);
                }

                // Additional logic related to canceling an order

                _unitOfWork.OrderRepository.DeleteOrderAsync(order);
                await _unitOfWork.SaveChangesAsync();

                return ApiResponse<string>.Success($"Order with id {id} canceled successfully", "Order canceled successfully", 200);
            }
            catch (Exception ex)
            {
                // Log the exception for further investigation
                Console.WriteLine($"Error canceling order: {ex.Message}");

                return ApiResponse<string>.Failed(false, "An error occurred while canceling the order", 500, new List<string> { ex.Message });
            }
        }
    }
}
