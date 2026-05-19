using OfficeWorkTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Application
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);

        Task<User?> GetByIdAsync(Guid id);

        Task<List<User>> GetAllAsync();

        Task DeleteAsync(User user);
    }
}