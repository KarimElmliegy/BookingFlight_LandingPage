using Booking.Core.Models.Booking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Core.Repository.contract
{
    public interface IContactUsRepository : IGenericRepository<ContactUsMessage>
    {
        Task<IEnumerable<ContactUsMessage>> GetMessagesByUserIdAsync(int userId);
        Task<ContactUsMessage?> GetMessageWithUserAsync(int id);
    }
}
