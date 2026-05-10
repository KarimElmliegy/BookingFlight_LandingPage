using Booking.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Booking.Core.Models.Booking
{
    public class ContactUsMessage : BaseEntity
    {
        [Required]
        public int UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required, MaxLength(150)]
        public string FullName { get; set; }

        [MaxLength(150)]
        public string Company { get; set; }

        [Required, MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(30)]
        public string PhoneNumber { get; set; }

        [MaxLength(250)]
        public string Address { get; set; }

        [Required, MaxLength(2000)]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
