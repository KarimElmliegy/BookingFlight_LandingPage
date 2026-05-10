using AutoMapper;
using Booking.Core.DTO;
using Booking.Core.Models.Booking;
using Booking.Core.Repository.contract;
using Booking.Core.Services.Contract;
using System.Threading.Tasks;

namespace Booking_Infrastructure.Services
{
    public class ContactUsService : IContactUsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactUsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task SendMessageAsync(int userId, CreateContactUsDto dto)
        {
            var message = _mapper.Map<ContactUsMessage>(dto);
            message.UserId = userId;

            await _unitOfWork.ContactUsRepository.AddAsync(message);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}