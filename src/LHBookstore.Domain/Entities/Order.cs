using LHBookstore.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace LHBookstore.Domain.Entities
{
    public class Order : BaseEntity
    {
        public List<OrderItem> OrderItems { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
