using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Booking.Core.Models.Booking;
namespace Booking.Core.Models.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public required string DisplayName { get; set; }
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        public ICollection<ContactUsMessage> ContactUsMessages { get; set; } = new List<ContactUsMessage>();

    }
}
