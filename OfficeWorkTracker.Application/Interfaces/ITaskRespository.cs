using OfficeWorkTracker.Application.DTOs.Task;
using OfficeWorkTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeWorkTracker.Application.Interfaces
{
    public interface ITaskRespository
    {
        Task<TaskItem> CreateAsync(TaskItem task);

        Task<List<TaskItem>> GetAllAsync();

        Task<TaskItem?> GetByIdAsync(int id);

        Task<bool> DeleteAsync(int id);

        Task<TaskItem> UpdateAsync(int id, UpdateTaskDto dto);
        Task<int> GetTotalTasksByUserAsync(int id);
        Task<int> GetPendingTaskByUserAsync(int id);
        Task<int> GetInProgressTaskByUserAsync(int id);
        Task<int> GetCompletedTaskByUserAsync(int id);
    }
}
