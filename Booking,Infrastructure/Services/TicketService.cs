using AutoMapper;
using Booking.Core.DTO;
using Booking.Core.Models.Booking;
using Booking.Core.Repository.contract;
using Booking.Core.Services.Contract;
using Booking.Core.Types; 
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

        public async Task<TicketDto> BookTripAsync(int userId, int tripId, int quantity)
        {
            if (quantity <= 0)
                throw new Exception("Quantity must be greater than zero");

            var trip = await _unitOfWork.TripRepository.GetByIdAsync(tripId);
            if (trip is null)
                throw new Exception("Trip not found");

            var userOrderBasket = await _unitOfWork.OrderRepository.GetUserPandingOrderAsync(userId);

            if (userOrderBasket == null)
            {
                userOrderBasket = new Order
                {
                    UserId = userId,
                    Status = OrderStatus.Pending,
                    TotalAmount = 0m,
                    Tickets = new List<Ticket>()
                };

                await _unitOfWork.Repository<Order>().AddAsync(userOrderBasket);
                await _unitOfWork.SaveChangesAsync(); // important so OrderId is generated
            }

            var existingTicket = await _unitOfWork.TicketRepository
                .GetPendingTicketForOrderAndTripAsync(userOrderBasket.Id, tripId);

            Ticket resultTicket;

            if (existingTicket != null)
            {
                existingTicket.Quantity += quantity;
                existingTicket.Price += trip.Price * quantity;
                resultTicket = existingTicket;
            }
            else
            {
                var ticket = new Ticket
                {
                    UserId = userId,
                    TripId = tripId,
                    BookingDate = DateTime.UtcNow,
                    ImageUrl = trip.ImageUrl,
                    Status = "Pending",
                    Quantity = quantity,
                    Price = trip.Price * quantity,
                    OrderId = userOrderBasket.Id
                };

                await _unitOfWork.TicketRepository.AddAsync(ticket);
                resultTicket = ticket;
            }

            userOrderBasket.TotalAmount += trip.Price * quantity;
            _unitOfWork.OrderRepository.Update(userOrderBasket);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TicketDto>(resultTicket);
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
            var ticket = await _unitOfWork.TicketRepository.GetTicketWithDetailsAsync(ticketId);
            if (ticket is null)
                throw new Exception("Ticket not found");

            var order = ticket.Order;

            if (order != null)
            {
                order.TotalAmount -= ticket.Price;

                if (order.Tickets != null)
                {
                    order.Tickets.Remove(ticket);
                }

                if (order.Tickets == null || !order.Tickets.Any())
                {
                    _unitOfWork.OrderRepository.Delete(order);
                }
            }

            _unitOfWork.TicketRepository.Delete(ticket);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}