using Microsoft.EntityFrameworkCore;
using OfficeWorkTracker.Application.Common;
using OfficeWorkTracker.Application.DTOs.Task;
using OfficeWorkTracker.Application.Exception;
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
    public class TaskRepository : ITaskRespository
    {
        private readonly ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            if (task == null)
                throw new NotFoundExecption("Task not found");
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public Task<TaskItem?> GetByIdAsync(int id)
        {
            return _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<int> GetCompletedTaskByUserAsync(int id)
        {
            return _context.Tasks.CountAsync(x => x.UserId == id && x.Status == Domain.Enum.WorkTaskStatus.Completed);
        }

        public async Task<int> GetInProgressTaskByUserAsync(int id)
        {
            return await _context.Tasks.CountAsync(x => x.UserId == id && x.Status == Domain.Enum.WorkTaskStatus.InProgress);
        }

        public async Task<int> GetPendingTaskByUserAsync(int id)
        {
            return await _context.Tasks.CountAsync(x => x.UserId == id && x.Status == Domain.Enum.WorkTaskStatus.Pending);
        }

        public async Task<int> GetTotalTasksByUserAsync(int id)
        {
            return await _context.Tasks.CountAsync(x => x.UserId == id);
        }

        public async Task<TaskItem> UpdateAsync(int id, UpdateTaskDto dto)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);

            if (task == null)
                return null;

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Status = (Domain.Enum.WorkTaskStatus)dto.Status;
            task.Priority = (Domain.Enum.TaskPriority)dto.Priority;
            task.DueDate = dto.DueDate;
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task UpdateAsync(TaskItem task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }
        public async Task<int> GetTotalTaskCountAsync()
        {
            return await _context.Tasks.CountAsync();
        }

        public async Task<int> GetTaskCountByStatusAsync(Domain.Enum.WorkTaskStatus status)
        {
            return await _context.Tasks.CountAsync(x => x.Status == status);
        }

        public async Task AddCommentAsync(TaskComment comment)
        {
            await _context.TaskComments.AddAsync(comment);

            await _context.SaveChangesAsync();
        }

        public async Task<List<TaskActivity>> GetTaskActivitiesAsync(int taskId)
        {
            return await _context.TaskActivities
                            .Where(x => x.TaskId == taskId)
                            .Include(x => x.User)
                            .OrderByDescending(x => x.CreatedDate)
                            .ToListAsync();
        }

        public async Task AddActivityAsnyc(TaskActivity activity)
        {
            await _context.TaskActivities.AddAsync(activity);

            await _context.SaveChangesAsync();
        }

        public async Task<List<TaskItem>> GetAllTaskWithUserAsync()
        {
            return await _context.Tasks.Include(x => x.User).ToListAsync();
        }

        public async Task<PageResponse<TaskResponseDto>> GetFilteredTaskAsync(TaskFilterDto filter)
        {
            // Step 1: Start building the query
            IQueryable<TaskItem> query = _context.Tasks
                .Include(t => t.User)
                .AsQueryable();

            // Step 2: Apply Search Filter
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                query = query.Where(t =>
                    t.Title.Contains(filter.Search) ||
                    t.Description.Contains(filter.Search));
            }

            // Step 3: Apply Status Filter
            if (filter.Status.HasValue)
            {
                query = query.Where(t => t.Status == filter.Status.Value);
            }

            // Step 4: Apply Priority Filter
            if (filter.Priority.HasValue)
            {
                query = query.Where(t => t.Priority == filter.Priority.Value);
            }

            // Step 5: Apply User Filter
            if (filter.UserId.HasValue)
            {
                query = query.Where(t => t.UserId == filter.UserId.Value);
            }

            // Step 6: Apply Due Date Filter
            if (filter.DueDate.HasValue)
            {
                query = query.Where(t =>
                    t.DueDate.HasValue &&
                    t.DueDate.Value.Date == filter.DueDate.Value.Date);
            }

            // Step 7: Apply Sorting
            query = filter.SortBy?.ToLower() switch
            {
                "title" => filter.SortDirection.ToLower() == "desc"
                    ? query.OrderByDescending(t => t.Title)
                    : query.OrderBy(t => t.Title),

                "priority" => filter.SortDirection.ToLower() == "desc"
                    ? query.OrderByDescending(t => t.Priority)
                    : query.OrderBy(t => t.Priority),

                "status" => filter.SortDirection.ToLower() == "desc"
                    ? query.OrderByDescending(t => t.Status)
                    : query.OrderBy(t => t.Status),

                "duedate" => filter.SortDirection.ToLower() == "desc"
                    ? query.OrderByDescending(t => t.DueDate)
                    : query.OrderBy(t => t.DueDate),

                "createddate" => filter.SortDirection.ToLower() == "desc"
                    ? query.OrderByDescending(t => t.CreatedDate)
                    : query.OrderBy(t => t.CreatedDate),

                _ => query.OrderByDescending(t => t.CreatedDate)
            };

            // Step 8: Get Total Records
            var totalRecords = await query.CountAsync();

            // Step 9: Apply Pagination
            var tasks = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            // Step 10: Map Entity to DTO
            var response = new PageResponse<TaskResponseDto>
            {
                Data = tasks.Select(task => new TaskResponseDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Status = task.Status,
                    Priority = task.Priority,
                    CreatedDate = task.CreatedDate,
                    DueDate = task.DueDate,
                    UserId = task.UserId
                }).ToList(),

                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)filter.PageSize)
            };

            return response;
        }
    }   
}
