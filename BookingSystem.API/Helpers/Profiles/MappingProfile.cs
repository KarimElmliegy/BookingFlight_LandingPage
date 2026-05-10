using AutoMapper;
using Booking.Core.DTO;
using Booking.Core.Models.Booking;

namespace BookingSystem.API.Helpers.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Trip, TripDto>().ReverseMap();
            CreateMap<CreateTripDto, Trip>();
            CreateMap<UpdateTripDto, Trip>();
            CreateMap<Ticket, TicketDto>()
                .ForMember(dest => dest.FromCity, opt => opt.MapFrom(src => src.Trip.FromCity))
                .ForMember(dest => dest.ToCity, opt => opt.MapFrom(src => src.Trip.ToCity))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
        }
    }
}
