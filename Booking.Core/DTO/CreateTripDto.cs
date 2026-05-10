using System.ComponentModel.DataAnnotations;

namespace Booking.Core.DTO
{
    public class CreateTripDto
    {
        [Required(ErrorMessage = "From city is required.")]
        [MaxLength(100, ErrorMessage = "From city cannot exceed 100 characters.")]
        public string FromCity { get; set; }

        [Required(ErrorMessage = "To city is required.")]
        [MaxLength(100, ErrorMessage = "To city cannot exceed 100 characters.")]
        public string ToCity { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Image URL is required.")]
        [MaxLength(500, ErrorMessage = "Image URL cannot exceed 500 characters.")]
        public string ImageUrl { get; set; }
    }
}