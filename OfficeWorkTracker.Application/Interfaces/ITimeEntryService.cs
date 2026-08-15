using OfficeWorkTracker.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Application.Interfaces
{
    public interface ITimeEntryService
    {
        Task<TimeENtryResponseDto> StartTimeENtryAsync(int id);
        Task<TimeENtryResponseDto> PauseTimeEntryAsync(int UserId);
    }
}
