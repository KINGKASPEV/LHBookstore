using System.Text.Json.Serialization;

namespace LHBookstore.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public string OrderId { get; set; } = Guid.NewGuid().ToString();
        public string BookId { get; set; } 
        public int Quantity { get; set; }
        public Book Book { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
    }
}
