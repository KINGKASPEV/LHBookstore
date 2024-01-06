namespace LHBookstore.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public string OrderId { get; set; } = Guid.NewGuid().ToString();
        public string BookId { get; set; } = Guid.NewGuid().ToString();
        public int Quantity { get; set; }
        public Book Book { get; set; }
        public Order Order { get; set; }
    }
}
