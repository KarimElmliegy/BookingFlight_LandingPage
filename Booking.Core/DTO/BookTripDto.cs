using System.ComponentModel.DataAnnotations;

namespace Booking.Core.DTOs
{
    public class BookTripDto
    {
        [Required(ErrorMessage = "Trip Id is required.")]
        public int TripId { get; set; }

        public int Quantity { get; set; }
    }
}