using Booking.Core.Models.Booking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Core.Repository.contract
{
    public interface ITripRepository : IGenericRepository<Trip>
    {
        Task<IEnumerable<Trip>> GetPopularTripsAsync();
        Task<IEnumerable<Trip>> SearchAsync(string fromCity, string toCity);
    }
}
