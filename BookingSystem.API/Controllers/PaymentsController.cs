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
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService; 
        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [HttpPost]
        public async Task<ActionResult<OrderDto?>> CreateOrUpdatePaymentIntentAsync(int basketId)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var Order =  await _paymentService.CreateOrUpdatePaymentIntentAsync(basketId,int.Parse(userIdValue)) ;
            return Ok(Order); 
        }






    }
}
