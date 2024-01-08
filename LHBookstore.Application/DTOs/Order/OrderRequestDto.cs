using System.ComponentModel.DataAnnotations;

namespace LHBookstore.Application.DTOs.Order
{
    public class OrderRequestDto
    {
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }
    }
}
