using AutoMapper;
using Booking.Core.Models.Identity;
using Booking.Core.Repository.contract;
using Booking.Core.Services.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking_Infrastructure.Services
{
    public class ServiceManager(
     UserManager<ApplicationUser> userManager,
     IConfiguration configuration,
     IUnitOfWork unitOfWork,
     IMapper mapper) : IServiceManager
    {
        private readonly Lazy<IAuthService> _authService = new(() => new AuthService(userManager, configuration));

        private readonly Lazy<ITripService> _tripService = new(() => new TripService(unitOfWork, mapper));

        private readonly Lazy<ITicketService> _ticketService = new(() => new TicketService(unitOfWork, mapper));

        private readonly Lazy<IContactUsService> _contactUsService = new(() => new ContactUsService(unitOfWork, mapper));

        public IAuthService AuthService => _authService.Value;
        public ITripService TripService => _tripService.Value;
        public ITicketService TicketService => _ticketService.Value;
        public IContactUsService ContactUsService => _contactUsService.Value;
    }
}
