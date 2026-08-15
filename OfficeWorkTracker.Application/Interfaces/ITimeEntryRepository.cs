using OfficeWorkTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Application.Interfaces
{
    public interface ITimeEntryRepository
    {
        Task<TimeEntry> GetActiveTimeEntryAsync(int id);
        Task AddAsync(TimeEntry timeEntry);
        Task UpdateAsync(TimeEntry timeEntry);
        Task SaveChangesAsync();
    }
}
