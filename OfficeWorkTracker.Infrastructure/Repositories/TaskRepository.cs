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
                return false;  
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
    }
}
