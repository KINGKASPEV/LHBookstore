using AutoMapper;
using LHBookstore.Application.DTOs.Order;
using LHBookstore.Application.Interfaces.Repositories;
using LHBookstore.Application.Interfaces.Services;
using LHBookstore.Application.Utilities;
using LHBookstore.Domain;
using LHBookstore.Domain.Entities;
using LHBookstore.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace LHBookstore.Application.ServiceImplementations
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<OrderService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<PageResult<List<OrderResponseDto>>>> GetAllOrdersAsync(int page, int perPage)
        {
            try
            {
                if (page <= 0 || perPage <= 0)
                {
                    return ApiResponse<PageResult<List<OrderResponseDto>>>.Failed(false, "Page and PerPage must be greater than zero", 400, null);
                }
                var orders = await _unitOfWork.OrderRepository.GetAllOrdersAsync();

                var paginatedOrders = await Pagination<Order>.GetPager(orders, perPage, page, b => b.Id, b => b.Id);

                var orderDtos = _mapper.Map<List<OrderResponseDto>>(paginatedOrders.Data);

                orderDtos.ForEach(dto => dto.Quantity = dto.OrderItems.Sum(item => item.Quantity));

                var pageResult = new PageResult<List<OrderResponseDto>>
                {
                    Data = orderDtos,
                    TotalPageCount = paginatedOrders.TotalPageCount,
                    CurrentPage = paginatedOrders.CurrentPage,
                    PerPage = paginatedOrders.PerPage,
                    TotalCount = paginatedOrders.TotalCount
                };

                return ApiResponse<PageResult<List<OrderResponseDto>>>.Success(pageResult, "Orders retrieved successfully", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving orders: {ex.Message}");
                return ApiResponse<PageResult<List<OrderResponseDto>>>.Failed(false, "An error occurred while retrieving orders", 500, new List<string> { ex.Message });
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

                orderDto.Quantity = orderDto.OrderItems.Sum(item => item.Quantity);

                return ApiResponse<OrderResponseDto>.Success(orderDto, "Order retrieved successfully", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving orders: {ex.Message}");
                return ApiResponse<OrderResponseDto>.Failed(false, "An error occurred while retrieving the order", 500, new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponse<OrderResponseDto>> PlaceOrderAsync(OrderRequestDto orderRequest, string bookId)
        {
            try
            {
                if (orderRequest == null || string.IsNullOrEmpty(bookId))
                {
                    return ApiResponse<OrderResponseDto>.Failed(false, "Order request or book ID is null or empty", 400, null);
                }

                if (orderRequest.Quantity < 1)
                {
                    return ApiResponse<OrderResponseDto>.Failed(false, "Quantity must be greater than 0 before an order can be placed", 400, null);
                }
                var book = await _unitOfWork.BookRepository.GetBookByIdAsync(bookId);
                if (book == null)
                {
                    return ApiResponse<OrderResponseDto>.Failed(false, $"Book with ID {bookId} not found", 404, null);
                }

                if (orderRequest.Quantity > book.QuantityAvailable)
                {
                    return ApiResponse<OrderResponseDto>.Failed(false, $"Not enough stock available. Available quantity: {book.QuantityAvailable}", 400, null);
                }
                var orderItem = new OrderItem
                {
                    Quantity = orderRequest.Quantity,
                    BookId = bookId,
                };
                var orderItemList = new List<OrderItem>();
                orderItemList.Add(orderItem);
                var order = new Order
                {
                    OrderItems = orderItemList,
                    OrderStatus = 0
                };

                book.QuantityAvailable -= orderRequest.Quantity;
                await _unitOfWork.BookRepository.UpdateBookAsync(book);

                await _unitOfWork.OrderRepository.AddOrderAsync(order);
                await _unitOfWork.SaveChangesAsync();

                var orderResponse = _mapper.Map<OrderResponseDto>(order);

                return ApiResponse<OrderResponseDto>.Success(orderResponse, "Order placed successfully", 201);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error placing order: {ex.Message}. Inner Exception: {ex.InnerException?.Message}");
                return ApiResponse<OrderResponseDto>.Failed(false, "An error occurred while placing the order", 500, new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponse<OrderResponseDto>> UpdateOrderAsync(string id, OrderRequestDto orderRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return ApiResponse<OrderResponseDto>.Failed(false, "Order id cannot be null or empty", 400, null);
                }

                if (orderRequest.Quantity < 1)
                {
                    return ApiResponse<OrderResponseDto>.Failed(false, "Quantity must be greater than 0", 400, null);
                }

                var existingOrder = await _unitOfWork.OrderRepository.GetOrderByIdAsync(id);

                if (existingOrder == null)
                {
                    return ApiResponse<OrderResponseDto>.Failed(false, $"Order with id {id} not found", 404, null);
                }
                if (orderRequest.Quantity > existingOrder.OrderItems.Sum(item => item.Quantity))
                {
                    var quantityDifference = orderRequest.Quantity - existingOrder.OrderItems.Sum(item => item.Quantity);

                    var book = existingOrder.OrderItems.FirstOrDefault()?.Book;

                    if (book != null && quantityDifference > book.QuantityAvailable)
                    {
                        return ApiResponse<OrderResponseDto>.Failed(false, $"Not enough stock available. Available quantity: {book.QuantityAvailable}", 400, null);
                    }

                    foreach (var orderItem in existingOrder.OrderItems)
                    {
                        orderItem.Quantity = orderRequest.Quantity;
                    }
                }
                _mapper.Map(orderRequest, existingOrder);

                await _unitOfWork.OrderRepository.UpdateOrderAsync(existingOrder);
                await _unitOfWork.SaveChangesAsync();

                var updatedOrderResponse = _mapper.Map<OrderResponseDto>(existingOrder);

                return ApiResponse<OrderResponseDto>.Success(updatedOrderResponse, "Order updated successfully", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating order: {ex.Message}");
                return ApiResponse<OrderResponseDto>.Failed(false, "An error occurred while updating the order", 500, new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponse<PageResult<List<OrderResponseDto>>>> GetOrdersByStatusAsync(string orderStatus, int page, int perPage)
        {
            try
            {
                if (page <= 0 || perPage <= 0)
                {
                    return ApiResponse<PageResult<List<OrderResponseDto>>>.Failed(false, "Page and PerPage must be greater than zero", 400, null);
                }

                if (!Enum.TryParse<OrderStatus>(orderStatus, out var parsedStatus))
                {
                    return ApiResponse<PageResult<List<OrderResponseDto>>>.Failed(false, $"Invalid order status: {orderStatus}", 400, null);
                }

                var orders = await _unitOfWork.OrderRepository.FindOrdersAsync(o => o.OrderStatus == parsedStatus);

                var paginatedOrders = await Pagination<Order>.GetPager(orders, perPage, page, b => b.Id, b => b.Id);

                var orderDtos = _mapper.Map<List<OrderResponseDto>>(paginatedOrders.Data);

                orderDtos.ForEach(dto => dto.Quantity = dto.OrderItems.Sum(item => item.Quantity));

                var pageResult = new PageResult<List<OrderResponseDto>>
                {
                    Data = orderDtos,
                    TotalPageCount = paginatedOrders.TotalPageCount,
                    CurrentPage = paginatedOrders.CurrentPage,
                    PerPage = paginatedOrders.PerPage,
                    TotalCount = paginatedOrders.TotalCount
                };

                return ApiResponse<PageResult<List<OrderResponseDto>>>.Success(pageResult, "Orders retrieved successfully", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving orders by status: {ex.Message}");
                return ApiResponse<PageResult<List<OrderResponseDto>>>.Failed(false, "An error occurred while retrieving orders by status", 500, new List<string> { ex.Message });
            }
        }




        public async Task<ApiResponse<string>> CancelOrderAsync(string orderId)
        {
            try
            {
                if (string.IsNullOrEmpty(orderId))
                {
                    return ApiResponse<string>.Failed(false, "Order id cannot be null or empty", 400, null);
                }

                var order = await _unitOfWork.OrderRepository.GetOrderByIdAsync(orderId);

                if (order == null)
                {
                    return ApiResponse<string>.Failed(false, $"Order with id {orderId} not found", 404, null);
                }
                await _unitOfWork.OrderRepository.DeleteOrderAsync(order);
                await _unitOfWork.SaveChangesAsync();

                return ApiResponse<string>.Success($"Order with id {orderId} canceled successfully", "Order canceled successfully", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error canceling order: {ex.Message}");
                return ApiResponse<string>.Failed(false, "An error occurred while canceling the order", 500, new List<string> { ex.Message });
            }
        }
    }
}
