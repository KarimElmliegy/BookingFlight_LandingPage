using Booking.Core.DTO;
using Booking.Core.DTOs;
using Booking.Core.Services;
using Booking.Core.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public TicketsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("book")]
        public async Task<ActionResult> BookTrip([FromBody] BookTripDto dto)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdValue, out var userId))
                return Unauthorized();

            var ticket = await _serviceManager.TicketService.BookTripAsync(userId, dto.TripId,dto.Quantity);
            return Ok(ticket);
        }

        [HttpGet("my-tickets")]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetMyTickets()
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdValue, out var userId))
                return Unauthorized();

            var tickets = await _serviceManager.TicketService.GetUserTicketsAsync(userId);
            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTicketDetails(int id)
        {
            var ticket = await _serviceManager.TicketService.GetTicketDetailsAsync(id);

            if (ticket is null)
                return NotFound();

            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdValue, out var userId))
                return Unauthorized();

            if (ticket.UserId != userId)
                return Forbid();

            return Ok(ticket);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> CancelTicket(int id)
        {
            var ticket = await _serviceManager.TicketService.GetTicketDetailsAsync(id);

            if (ticket is null)
                return NotFound();

            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdValue, out var userId))
                return Unauthorized();

            if (ticket.UserId != userId)
                return Forbid();

            await _serviceManager.TicketService.CancelTicketAsync(id);
            return NoContent();
        }
    }
}