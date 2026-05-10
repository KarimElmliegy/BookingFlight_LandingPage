using Booking.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Core.Repository.contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
