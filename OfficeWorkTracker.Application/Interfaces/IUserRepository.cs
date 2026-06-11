using OfficeWorkTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Application
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);

        Task<User?> GetByIdAsync(int id);

        Task<List<User>> GetAllAsync();
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);

        Task<User?> GetByEmailAsync(string email);
        Task<int> GetTotalUserCountAsync();
    }
}