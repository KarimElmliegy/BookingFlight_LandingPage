using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Core.DTO
{
    public class TicketDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }

        public int TripId { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }

        public DateTime BookingDate { get; set; }
        public string Status { get; set; }
    }
}
