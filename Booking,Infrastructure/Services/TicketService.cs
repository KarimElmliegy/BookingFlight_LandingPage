using AutoMapper;
using Booking.Core.DTO;
using Booking.Core.Models.Booking;
using Booking.Core.Repository.contract;
using Booking.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking_Infrastructure.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TicketService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TicketDto> BookTripAsync(int userId, int tripId)
        {
            var trip = await _unitOfWork.TripRepository.GetByIdAsync(tripId);

            if (trip is null)
                throw new Exception("Trip not found");

            var ticket = new Ticket
            {
                UserId = userId,
                TripId = tripId,
                BookingDate = DateTime.UtcNow,
                Status = "Pending"
            };

            await _unitOfWork.TicketRepository.AddAsync(ticket);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TicketDto>(ticket);
        }

        public async Task<IEnumerable<TicketDto>> GetUserTicketsAsync(int userId)
        {
            var tickets = await _unitOfWork.TicketRepository.GetTicketsByUserIdAsync(userId);
            return tickets.Select(t => _mapper.Map<TicketDto>(t));
        }

        public async Task<TicketDto?> GetTicketDetailsAsync(int ticketId)
        {
            var ticket = await _unitOfWork.TicketRepository.GetTicketWithDetailsAsync(ticketId);
            return ticket is null ? null : _mapper.Map<TicketDto>(ticket);
        }

        public async Task CancelTicketAsync(int ticketId)
        {
            var ticket = await _unitOfWork.TicketRepository.GetByIdAsync(ticketId);

            if (ticket is null)
                throw new Exception("Ticket not found");

            _unitOfWork.TicketRepository.Delete(ticket);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}