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
        public string ImageUrl { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }

        public int Quantity {  get; set; }
        public int? OrderId { get; set; }
        public Order? Order { get; set; }

        public decimal Price { get; set; }

    }
}
