using Booking.Core.DTO;
using Booking.Core.Models.Booking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Core.Services.Contract
{
    public interface ITripService
    {
        Task<IEnumerable<TripDto>> GetAllTripsAsync();
        Task<TripDto?> GetTripByIdAsync(int id);
        Task CreateTripAsync(CreateTripDto dto);
        Task UpdateTripAsync(int id, UpdateTripDto dto);
        Task DeleteTripAsync(int id);
    }
}
