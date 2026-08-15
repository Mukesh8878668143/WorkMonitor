using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using OfficeWorkTracker.Application.Interfaces;
using OfficeWorkTracker.Domain.Entities;
using OfficeWorkTracker.Domain.Enum;
using OfficeWorkTracker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Infrastructure.Repositories
{
    public class TimeEntryRepository : ITimeEntryRepository
    {
        private readonly ApplicationDbContext _context;

        public TimeEntryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TimeEntry> GetActiveTimeEntryAsync(int id)
        {
            #pragma warning disable CS8603 // Possible null reference return.
            return await _context.TimeEntries.FirstOrDefaultAsync(te => te.UserId == id &&
            (te.Status == TimeEntryStatus.Running || te.Status == TimeEntryStatus.Paused));
            #pragma warning restore CS8603 // Possible null reference return.
        }
        
        public async Task AddAsync(TimeEntry timeEntry)
        {
            await _context.TimeEntries.AddAsync(timeEntry);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TimeEntry timeEntry)
        {
            _context.TimeEntries.Update(timeEntry);
            await _context.SaveChangesAsync();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
