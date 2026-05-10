using Booking.Core.DTO;
using Booking.Core.Models.Booking;
using Booking.Core.Repository.contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Core.Services.Contract
{
    public interface IContactUsService 
    {
        Task SendMessageAsync(int userId, CreateContactUsDto dto);
    }
}
