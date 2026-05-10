using AutoMapper;
using Booking.Core.DTO;
using Booking.Core.Models.Booking;
using Booking.Core.Repository.contract;
using Booking.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking_Infrastructure.Services
{
    public class TripService : ITripService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TripService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TripDto>> GetAllTripsAsync()
        {
            var trips = await _unitOfWork.TripRepository.GetAllAsync();
            return trips.Select(t => _mapper.Map<TripDto>(t));
        }

        public async Task<TripDto?> GetTripByIdAsync(int id)
        {
            var trip = await _unitOfWork.TripRepository.GetByIdAsync(id);
            return trip is null ? null : _mapper.Map<TripDto>(trip);
        }

        public async Task CreateTripAsync(CreateTripDto dto)
        {
            var trip = _mapper.Map<Trip>(dto);

            await _unitOfWork.TripRepository.AddAsync(trip);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateTripAsync(int id, UpdateTripDto dto)
        {
            var trip = await _unitOfWork.TripRepository.GetByIdAsync(id);

            if (trip is null)
                throw new Exception("Trip not found");

            _mapper.Map(dto, trip);

            _unitOfWork.TripRepository.Update(trip);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTripAsync(int id)
        {
            var trip = await _unitOfWork.TripRepository.GetByIdAsync(id);

            if (trip is null)
                throw new Exception("Trip not found");

            _unitOfWork.TripRepository.Delete(trip);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}