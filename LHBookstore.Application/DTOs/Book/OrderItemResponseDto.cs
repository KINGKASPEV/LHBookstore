namespace LHBookstore.Application.DTOs.Book
{
    public class OrderItemResponseDto
    {
        public string OrderId { get; set; }
        public string BookId { get; set; }
        public int Quantity { get; set; }
    }
}
