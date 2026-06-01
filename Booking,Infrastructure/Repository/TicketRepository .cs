using Booking.Core.Data;
using Booking.Core.Models.Booking;
using Booking.Core.Repository.contract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking_Infrastructure.Repository
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(int userId)
        {
            return await _context.Tickets
                .Where(t => t.UserId == userId)
                .Include(t => t.Trip)
                .ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByTripIdAsync(int tripId)
        {
            return await _context.Tickets
                .Where(t => t.TripId == tripId)
                .Include(t => t.User)
                .ToListAsync();
        }

        public async Task<Ticket?> GetTicketWithDetailsAsync(int ticketId)
        {
            return await _context.Tickets
                .Include(t => t.User)
                .Include(t => t.Trip)
                .Include(t => t.Order)
                .ThenInclude(t => t.Tickets) 
                .FirstOrDefaultAsync(t => t.Id == ticketId);
        }

        public async Task<Ticket?> GetPendingTicketForOrderAndTripAsync(int orderId, int tripId)
        {
            return await _context.Tickets.FirstOrDefaultAsync(t => t.OrderId == orderId && t.TripId == tripId);
        }
    }
}