using LHBookstore.Domain.Entities;

namespace LHBookstore.Application.DTOs.Order
{
    public class OrderRequestDto
    {
        public List<OrderItem> OrderItems { get; set; }
    }
}
