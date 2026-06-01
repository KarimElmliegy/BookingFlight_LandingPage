using AutoMapper;
using Booking.Core.DTO;
using Booking.Core.Models.Booking;
using Booking.Core.Repository.contract;
using Booking.Core.Services.Contract;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Booking_Infrastructure.Services
{
    public class PaymentService(IConfiguration _configuration,IUnitOfWork _unitOfWork,IMapper _mapper) : IPaymentService
    {

        public async Task<OrderDto?> CreateOrUpdatePaymentIntentAsync(int orderId, int userId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];

            var order = await _unitOfWork.OrderRepository.GetOrderWithId(orderId, userId);
            if (order == null)
                return null;

            var paymentIntentService = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(order.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(order.TotalAmount * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                paymentIntent = await paymentIntentService.CreateAsync(options);

                order.PaymentIntentId = paymentIntent.Id;
                order.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                var updateOptions = new PaymentIntentUpdateOptions
                {
                    Amount = (long)(order.TotalAmount * 100)
                };

                await paymentIntentService.UpdateAsync(order.PaymentIntentId, updateOptions);
            }

            _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderDto>(order);
        }
    }
}
