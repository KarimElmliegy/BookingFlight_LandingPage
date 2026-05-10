using Booking.Core.Data;
using Booking.Core.Models.Booking;
using Booking.Core.Repository.contract;
using Booking_Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking_Infrastructure.Repository
{
    public class TripRepository : GenericRepository<Trip>, ITripRepository
    {
        public TripRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Trip>> GetPopularTripsAsync()
        {
            return await _context.Trips
                .OrderByDescending(t => t.Price)
                .ToListAsync();
        }

        public async Task<IEnumerable<Trip>> SearchAsync(string fromCity, string toCity)
        {
            return await _context.Trips
                .Where(t => t.FromCity == fromCity && t.ToCity == toCity)
                .ToListAsync();
        }
    }
}