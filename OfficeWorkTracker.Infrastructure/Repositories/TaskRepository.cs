using Microsoft.EntityFrameworkCore;
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
            if(task == null)
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
           return _context.Tasks.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public Task<int> GetCompletedTaskByUserAsync(int id)
        {
            return _context.Tasks.CountAsync(x => x.UserId == id && x.Status == Domain.Enum.TaskStatus.Completed);
        }

        public async Task<int> GetInProgressTaskByUserAsync(int id)
        {
            return await _context.Tasks.CountAsync(x => x.UserId == id && x.Status == Domain.Enum.TaskStatus.InProgress);
        }

        public async Task<int> GetPendingTaskByUserAsync(int id)
        {
            return await _context.Tasks.CountAsync(x => x.UserId == id && x.Status == Domain.Enum.TaskStatus.pending);
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
            task.Status = (Domain.Enum.TaskStatus)dto.Status;
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
    }
}
