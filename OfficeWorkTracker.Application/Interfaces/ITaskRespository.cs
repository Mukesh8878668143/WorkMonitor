using OfficeWorkTracker.Application.Common;
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
        Task UpdateAsync(TaskItem task);

        Task<int> GetTotalTaskCountAsync();

        Task<int> GetTaskCountByStatusAsync(Domain.Enum.WorkTaskStatus status);

        /// <summary>
        /// Method to add a comment to a task. This will create a new TaskComment entity and associate it with the specified task.
        /// </summary>
        /// <param name="comment">The TaskComment entity to be added.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddCommentAsync(TaskComment comment);

        Task AddActivityAsnyc(TaskActivity activity);
        Task<List<TaskActivity>> GetTaskActivitiesAsync(int taskid);
        Task<List<TaskItem>> GetAllTaskWithUserAsync();
        Task<PageResponse<TaskResponseDto>> GetFilteredTaskAsync(TaskFilterDto filter);
    }
}
