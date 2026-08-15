using Microsoft.EntityFrameworkCore;
using OfficeWorkTracker.Application.Interfaces;
using OfficeWorkTracker.Domain.Entities;
using OfficeWorkTracker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Infrastructure.Repositories
{
    public class TimeEntryBreakRepository : ITimeEntryBreakRepository
    {
        private readonly ApplicationDbContext _context;

        public TimeEntryBreakRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TimeEntryBreak timeEntryBreak)
        {
           await _context.TimeEntryBreaks.AddAsync(timeEntryBreak);
           await _context.SaveChangesAsync();
        }

        async Task ITimeEntryBreakRepository.AddAsync(TimeEntryBreak timeEntryBreak)
        {
            await _context.TimeEntryBreaks.AddAsync(timeEntryBreak);
        }

        async Task ITimeEntryBreakRepository.SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<TimeEntryBreak?> GetActiveBreakAsync(int timeEntryId)
        {
            return await _context.TimeEntryBreaks
                .FirstOrDefaultAsync(x =>
                    x.TimeEntryId == timeEntryId &&
                    x.BreakEndTime == null);
        }
    }
}
