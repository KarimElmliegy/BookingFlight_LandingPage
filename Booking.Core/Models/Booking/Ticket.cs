using Booking.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Core.Models.Booking
{
    public class Ticket : BaseEntity
    {

        public int UserId { get; set; }   // links to AspNetUsers
        public ApplicationUser User { get; set; }

        public int TripId { get; set; }      // links to Trips
        public Trip Trip { get; set; }

        public DateTime BookingDate { get; set; }
        public string Status { get; set; }
    }
}
