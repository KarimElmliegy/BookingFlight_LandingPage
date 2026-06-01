using Booking.Core.Models.Identity;
using Booking.Core.Types;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace Booking.Core.Models.Booking
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();


        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
