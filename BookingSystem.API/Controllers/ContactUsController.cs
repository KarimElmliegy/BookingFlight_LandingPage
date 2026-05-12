using Booking.Core.DTO;
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
    public class ContactUsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ContactUsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("send-message")]
        public async Task<IActionResult> SendMessage([FromBody] CreateContactUsDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdValue, out var userId))
                return Unauthorized();

            await _serviceManager.ContactUsService.SendMessageAsync(userId, dto);

            return Ok(new
            {
                Message = "Message sent successfully"
            });
        }
    }
}