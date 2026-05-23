using OfficeWorkTracker.Application.DTOs.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Application.Interfaces
{
    public interface ITaskService
    {

        Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto dto);

        Task<List<TaskResponseDto>> GetAllTasksAsync();

        Task<TaskResponseDto?> GetTaskByIdAsync(int id);

        Task<bool> DeleteTaskAsync(int id);
    }
}
