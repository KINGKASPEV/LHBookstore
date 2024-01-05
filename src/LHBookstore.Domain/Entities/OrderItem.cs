namespace LHBookstore.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public string OrderId { get; set; }
        public string BookId { get; set; }
        public int Quantity { get; set; }
        public Book Book { get; set; }
        public Order Order { get; set; }
    }
}
