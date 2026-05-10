using Booking.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Core.Repository.contract
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : BaseEntity;

        ITripRepository TripRepository { get; }

        ITicketRepository TicketRepository { get; }

        IContactUsRepository ContactUsRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
