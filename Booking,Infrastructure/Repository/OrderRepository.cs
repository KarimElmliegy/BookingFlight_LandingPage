using Booking.Core.Data;
using Booking.Core.Models.Booking;
using Booking.Core.Repository.contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Booking.Core.Types; 
namespace Booking_Infrastructure.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context)
           : base(context)
        {
        }

        public async Task<ICollection<Order>> GetAllOrderForUserAsync(int userId)
        {
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Tickets)
                .Include(o=>o.User)
                .ToListAsync();

            return orders;
        }
        public async Task<Order?> GetOrderWithId(int OrderId, int userId)
        {
            return await _context.Orders.Include(o => o.User).Include(t => t.Tickets).FirstOrDefaultAsync(o=>o.UserId==userId&&o.Id==OrderId); 
        }
        public async Task<Order?> GetUserPandingOrderAsync(int userId)
        {
            return await _context.Orders.Include(o=>o.User).Include(t=>t.Tickets).FirstOrDefaultAsync(t => t.UserId == userId && t.Status == OrderStatus.Pending); 
        }

    }
}
