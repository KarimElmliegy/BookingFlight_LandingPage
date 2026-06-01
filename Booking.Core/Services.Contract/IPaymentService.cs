using Booking.Core.DTO;
using Booking.Core.Models.Booking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Core.Services.Contract
{
    public interface IPaymentService
    {
        public Task<OrderDto?> CreateOrUpdatePaymentIntentAsync(int OrderId , int UserId); 
    }
}
