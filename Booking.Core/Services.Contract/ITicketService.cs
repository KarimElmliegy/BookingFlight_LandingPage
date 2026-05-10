using Booking.Core.DTO;
using Booking.Core.Models.Booking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Core.Services.Contract
{
    public interface ITicketService
    {
        Task<TicketDto> BookTripAsync(int userId, int tripId);
        Task<IEnumerable<TicketDto>> GetUserTicketsAsync(int userId);
        Task<TicketDto?> GetTicketDetailsAsync(int ticketId);
        Task CancelTicketAsync(int ticketId);
    }
}
