using Booking.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Core.Services.Contract
{
    public interface IOrderService
    {
        public Task<ICollection<OrderDto>> SGetAllOrderForUserAsync(int userid); 

        public Task DeleteOrder(int OrderId) ;   
    }
}
