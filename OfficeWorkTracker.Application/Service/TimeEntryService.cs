using OfficeWorkTracker.Application.DTOs;
using OfficeWorkTracker.Application.Interfaces;
using OfficeWorkTracker.Domain.Entities;
using OfficeWorkTracker.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Application.Service
{
    public class TimeEntryService:ITimeEntryService
    {
        private readonly ITimeEntryRepository _timeEntryRepository;
        private readonly ITimeEntryBreakRepository _timeEntryBreakRepository;
        private readonly IUserRepository _userRepository;
        public TimeEntryService(ITimeEntryRepository timeentryRepository,ITimeEntryBreakRepository timeentryBreakRepository,IUserRepository userRepository) 
        {
            _timeEntryRepository = timeentryRepository;
            _timeEntryBreakRepository = timeentryBreakRepository;
            _userRepository = userRepository;
        }

        public async Task<TimeENtryResponseDto> PauseTimeEntryAsync(int UserId)
        {
            var ActiveEntry = await _timeEntryRepository.GetActiveTimeEntryAsync(UserId);
            if (ActiveEntry == null)
                throw new System.Exception("No active time entry found");

            if (ActiveEntry.Status != TimeEntryStatus.Running)
                throw new System.Exception("Only running time entries can be paused.");

            var currentTime = DateTime.UtcNow;

            var brekEntry = new TimeEntryBreak
            { 
                TimeEntryId = ActiveEntry.Id,
                BreakStartTime = currentTime,
                CreatedDate = currentTime,
            };

            await _timeEntryBreakRepository.AddAsync(brekEntry);

            ActiveEntry.Status = TimeEntryStatus.Paused;

            await _timeEntryRepository.UpdateAsync(ActiveEntry);
            await _timeEntryRepository.SaveChangesAsync();

            return new TimeENtryResponseDto
            {
                Id = ActiveEntry.Id,
                UserId = ActiveEntry.UserId,
                WorkDate = ActiveEntry.WorkDate,
                StartTime = ActiveEntry.StartTime,
                EndTime = ActiveEntry.EndTime,
                TotalDuration = ActiveEntry.TotalDuration,
                Status = ActiveEntry.Status
            };

        }

        public async Task<TimeENtryResponseDto> StartTimeENtryAsync(int id)
        {
            var activeEntry = await _timeEntryRepository.GetActiveTimeEntryAsync(id);
            if(activeEntry != null)
            {
                throw new InvalidOperationException("You already have an active time entry.");
            }

            var user = await _userRepository.GetByIdAsync(id);
            if(user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var timeEntry = new TimeEntry
            {
                UserId = id,
                WorkDate = DateOnly.FromDateTime(DateTime.UtcNow),
                StartTime = DateTime.Now,
                Status = TimeEntryStatus.Running,
                TotalDuration = TimeSpan.Zero,
                CreatedDate = DateTime.UtcNow
            };

            await _timeEntryRepository.AddAsync(timeEntry);

            return new TimeENtryResponseDto
            {
                Id = timeEntry.Id,
                UserId = user.Id,
                UserName = user.FullName,
                WorkDate = timeEntry.WorkDate,
                StartTime = timeEntry.StartTime,
                EndTime = timeEntry.EndTime,
                TotalDuration = timeEntry.TotalDuration,
                Status = timeEntry.Status
            };
        }
    }
}
