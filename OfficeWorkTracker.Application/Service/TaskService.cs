using OfficeWorkTracker.Application.DTOs;
using OfficeWorkTracker.Application.DTOs.Task;
using OfficeWorkTracker.Application.Interfaces;
using OfficeWorkTracker.Domain.Entities;
using OfficeWorkTracker.Domain.Enum;

namespace OfficeWorkTracker.Application.Service
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRespository _taskRespository;
        private readonly IUserRepository _userRepository;

        public TaskService(ITaskRespository taskRespository, IUserRepository userRepository)
        {
            _taskRespository = taskRespository;
            _userRepository = userRepository;
        }

        public async Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                DueDate = dto.DueDate,
                UserId = dto.UserId,
                Status = Domain.Enum.TaskStatus.pending,
                CreatedDate = DateTime.UtcNow
            };

            var Useridcheck = _userRepository.GetByIdAsync(task.UserId);

            if (Useridcheck.Result != null)
            {
                var createdTask = await _taskRespository.CreateAsync(task);
                return new TaskResponseDto
                {
                    Id = createdTask.Id,
                    Title = createdTask.Title,
                    Description = createdTask.Description,
                    Priority = createdTask.Priority,
                    Status = createdTask.Status,
                    CreatedDate = createdTask.CreatedDate,
                    DueDate = createdTask.DueDate,
                    UserId = createdTask.UserId
                };
            }
            else
            {
                return null;
            }

        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            return await _taskRespository.DeleteAsync(id);
        }

        public Task<List<TaskResponseDto>> GetAllTasksAsync()
        {
            var tasks = _taskRespository.GetAllAsync().Result;

            return Task.FromResult(tasks.Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Priority = t.Priority,
                Status = t.Status,
                CreatedDate = t.CreatedDate,
                DueDate = t.DueDate,
                UserId = t.UserId
            }).ToList());
        }

        public Task<TaskResponseDto?> GetTaskByIdAsync(int id)
        {
            var task = _taskRespository.GetByIdAsync(id).Result;
            if (task == null)
                return Task.FromResult<TaskResponseDto?>(null);
            return Task.FromResult<TaskResponseDto?>(new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Priority = task.Priority,
                Status = task.Status,
                CreatedDate = task.CreatedDate,
                DueDate = task.DueDate,
                UserId = task.UserId
            });
        }

        public async Task<TaskResponseDto> UpdateTaskAsync(int id, UpdateTaskDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new ArgumentException("Title cannot be empty.");
            if (string.IsNullOrWhiteSpace(dto.Description))
                throw new ArgumentException("Description cannot be empty.");
            if (dto.DueDate == null)
                throw new ArgumentException("DueDate cannot be empty.");
            if (dto.Priority == null)
                throw new ArgumentException("Priority cannot be empty.");
            if (dto.Status == null)
                throw new ArgumentException("Status cannot be empty.");

            var updatedTask = await _taskRespository.UpdateAsync(id, dto);
            if (updatedTask == null)
                return null;

            return new TaskResponseDto
            {
                Id = updatedTask.Id,
                Title = updatedTask.Title,
                Description = updatedTask.Description,
                Priority = updatedTask.Priority,
                Status = updatedTask.Status,
                CreatedDate = updatedTask.CreatedDate,
                DueDate = updatedTask.DueDate,
                UserId = updatedTask.UserId
            };
        }
    }
}
