using Booking.Core.DTO;
using Booking.Core.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager; 
        public TripsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var trips = await _serviceManager.TripService.GetAllTripsAsync();
            return Ok(trips);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var trip = await _serviceManager.TripService.GetTripByIdAsync(id);

            if (trip is null)
                return NotFound();

            return Ok(trip);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateTripDto dto)
        {
            await _serviceManager.TripService.CreateTripAsync(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTripDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            await _serviceManager.TripService.UpdateTripAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceManager.TripService.DeleteTripAsync(id);
            return NoContent();
        }
    }
}