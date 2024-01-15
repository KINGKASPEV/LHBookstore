namespace LHBookstore.Application.DTOs.Book
{
    public class BookResponseDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int QuantityAvailable { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
