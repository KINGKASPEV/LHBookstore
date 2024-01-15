using LHBookstore.Domain.Enums;

namespace LHBookstore.Domain.Entities
{
    public class Order : BaseEntity
    {
        public List<OrderItem> OrderItems { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
