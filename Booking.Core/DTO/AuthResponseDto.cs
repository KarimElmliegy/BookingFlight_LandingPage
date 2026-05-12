using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Core.DTO
{
    public class AuthResponseDto
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
