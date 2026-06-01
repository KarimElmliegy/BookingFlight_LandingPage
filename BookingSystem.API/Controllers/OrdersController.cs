using Booking.Core.DTO;
using Booking.Core.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
          private readonly  IServiceManager _serviceManager;
        public OrdersController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        [HttpGet]
        public async Task<ActionResult<ICollection<OrderDto>>> GetOrders()
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _serviceManager.OrderService.SGetAllOrderForUserAsync(int.Parse(userIdValue));
            return Ok(result);  
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteOrder(int orderId)
        {
            await _serviceManager.OrderService.DeleteOrder(orderId);
            return Ok(); 
        }
    }
}
