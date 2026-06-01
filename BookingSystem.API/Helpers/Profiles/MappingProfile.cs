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
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.UserPhone, opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Tickets, opt => opt.MapFrom(src => src.Tickets));

            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Tickets, opt => opt.Ignore());
         
            CreateMap<CreateContactUsDto, ContactUsMessage>().ReverseMap();
        }
    }
}
