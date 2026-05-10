using Booking.Core.Models.Booking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Core.Repository.contract
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(int userId);
        Task<IEnumerable<Ticket>> GetTicketsByTripIdAsync(int tripId);
        Task<Ticket?> GetTicketWithDetailsAsync(int ticketId);

    }
}
