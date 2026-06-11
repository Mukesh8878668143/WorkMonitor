using Microsoft.EntityFrameworkCore;
using OfficeWorkTracker.Application;
using OfficeWorkTracker.Domain.Entities;
using OfficeWorkTracker.Infrastructure.Data;
public class UserRepository : IUserRepository
{
    private ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);

        await _context.SaveChangesAsync();
    }

    async Task<User?> IUserRepository.GetByIdAsync(int id)
    {

        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    async Task<List<User>> IUserRepository.GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);

        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<int> GetTotalUserCountAsync()
    {
        return await _context.Users.CountAsync();
    }
}