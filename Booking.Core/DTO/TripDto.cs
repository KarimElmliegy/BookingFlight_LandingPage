using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Core.DTO
{
    public class TripDto
    {
        public int Id { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
