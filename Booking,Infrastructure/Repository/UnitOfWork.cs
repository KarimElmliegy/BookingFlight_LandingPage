using Booking.Core.Data;
using Booking.Core.Models;
using Booking.Core.Repository.contract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Booking_Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private Hashtable _repositories;

        public ITripRepository TripRepository { get; }

        public ITicketRepository TicketRepository { get; }

        public IContactUsRepository ContactUsRepository { get; }

        public UnitOfWork(ApplicationDbContext context, ITripRepository tripRepository, ITicketRepository ticketRepository)
        {
            _context = context;
            TripRepository = tripRepository;
            TicketRepository = ticketRepository;
            _repositories = new Hashtable();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryInstance = new GenericRepository<TEntity>(_context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>) _repositories[type] ;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
