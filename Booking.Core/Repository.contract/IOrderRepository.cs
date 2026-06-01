using Booking.Core.Models.Booking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Core.Repository.contract
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public Task<Order?> GetUserPandingOrderAsync(int userId);

        public Task<Order?> GetOrderWithId(int OrderId, int userId);
        public Task<ICollection<Order>> GetAllOrderForUserAsync(int userId);
    }
}
