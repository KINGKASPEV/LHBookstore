using LHBookstore.Domain.Entities;
using LHBookstore.Domain.Enums;

namespace LHBookstore.Application.DTOs.Order
{
    public class OrderResponseDto
    {
        public string Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
