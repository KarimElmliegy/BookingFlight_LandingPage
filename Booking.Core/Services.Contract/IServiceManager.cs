using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Core.Services.Contract
{
    public interface IServiceManager
    {
        IAuthService AuthService { get; }
        ITripService TripService { get; }
        ITicketService TicketService { get; }
        IContactUsService ContactUsService { get; }
    }
}
