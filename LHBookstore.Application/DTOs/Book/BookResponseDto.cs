using System.ComponentModel.DataAnnotations;

namespace LHBookstore.Application.DTOs.Book
{
    public class BookResponseDto
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        [MaxLength(100, ErrorMessage = "Author cannot exceed 100 characters")]
        public string Author { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
