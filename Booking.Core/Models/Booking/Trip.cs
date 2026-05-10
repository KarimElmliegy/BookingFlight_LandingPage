using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Core.Models.Booking
{
    public class Trip : BaseEntity
    {
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
