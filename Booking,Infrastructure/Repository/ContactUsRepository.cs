using Booking.Core.Data;
using Booking.Core.Models.Booking;
using Booking.Core.Repository.contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking_Infrastructure.Repository
{
    public class ContactUsRepository : GenericRepository<ContactUsMessage>, IContactUsRepository
    {
        public ContactUsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ContactUsMessage>> GetMessagesByUserIdAsync(int userId)
        {
            return await _context.Set<ContactUsMessage>()
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<ContactUsMessage?> GetMessageWithUserAsync(int id)
        {
            return await _context.Set<ContactUsMessage>()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
