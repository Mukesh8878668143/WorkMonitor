using OfficeWorkTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Application.Interfaces
{
    public interface ITimeEntryBreakRepository
    {
        Task AddAsync(TimeEntryBreak timeEntryBreak);
        Task SaveChangesAsync();
        Task<TimeEntryBreak> GetActiveBreakAsync(int timeEntryId);
    }
}
