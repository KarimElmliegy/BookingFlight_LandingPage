using Booking.Core.Models.Booking;
using Booking.Core.Models.Identity;
using Booking.Core.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Core.DTO
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; }

        public string UserName { get; set; }

        public string UserPhone { get; set; }

        public OrderStatus Status { get; set; }

        public decimal TotalAmount { get; set; }
        public ICollection<TicketDto> Tickets { get; set; } = new List<TicketDto>();

    }
}
