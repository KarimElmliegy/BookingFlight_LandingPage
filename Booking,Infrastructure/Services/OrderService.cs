using AutoMapper;
using Booking.Core.DTO;
using Booking.Core.Repository.contract;
using Booking.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking_Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 
        public OrderService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task DeleteOrder(int OrderId)
        {
            var Order =  await _unitOfWork.OrderRepository.GetByIdAsync(OrderId);
            _unitOfWork.OrderRepository.Delete(Order); 
        }

        public async Task<ICollection<OrderDto>> SGetAllOrderForUserAsync (int userid)
        {
            var UserOrder = await _unitOfWork.OrderRepository.GetAllOrderForUserAsync(userid);
            return _mapper.Map<ICollection<OrderDto>>(UserOrder);
        }
    }
}
