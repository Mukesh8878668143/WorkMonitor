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
    }
}
