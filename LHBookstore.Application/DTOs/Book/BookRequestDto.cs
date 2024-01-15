using System.ComponentModel.DataAnnotations;

namespace LHBookstore.Application.DTOs.Book
{
    public class BookRequestDto
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        [MaxLength(100, ErrorMessage = "Author cannot exceed 100 characters")]
        public string Author { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }
        public int QuantityAvailable { get; set; }
        public bool IsAvailable { get; set; }
    }
}
