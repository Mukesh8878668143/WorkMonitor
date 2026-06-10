using OfficeWorkTracker.Application.DTOs;
using OfficeWorkTracker.Application.DTOs.Task;
using OfficeWorkTracker.Application.Exception;
using OfficeWorkTracker.Application.Interfaces;
using OfficeWorkTracker.Domain.Entities;
using OfficeWorkTracker.Domain.Enum;
using System.Runtime.InteropServices;
using TaskStatus = OfficeWorkTracker.Domain.Enum.TaskStatus;

namespace OfficeWorkTracker.Application.Service
{
    
    public class TaskService : ITaskService
    {
        private readonly ITaskRespository _taskRespository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;

        public TaskService(ITaskRespository taskRespository, IUserRepository userRepository, ICurrentUserService currentUserService)
        {
            _taskRespository = taskRespository;
            _userRepository = userRepository;
            _currentUserService = currentUserService;
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
               throw new NotFoundExecption("User Not found.");
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
                throw new ValidationException("Title cannot be empty.");
            if (string.IsNullOrWhiteSpace(dto.Description))
                throw new ValidationException("Description cannot be empty.");
            if (dto.DueDate == null)
                throw new ValidationException("DueDate cannot be empty.");
            if (dto.Priority == null)
                throw new ValidationException("Priority cannot be empty.");
            if (dto.Status == null)
                throw new ValidationException("Status cannot be empty.");
            var updatedTask = await _taskRespository.UpdateAsync(id, dto);
            if (updatedTask == null)
                throw new NotFoundExecption("Task not found.");

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

        public async Task<TaskResponseDto> AssignTaskAsync(AssignTaskRequestDto request)
        {
            var user = await _userRepository.GetByIdAsync(request.AssignedToUserID);
            if(user == null)
            {
                throw new NotFoundExecption("User not found.");
            }

            var task = new TaskItem
            {
                Title = request.Title,
                Description = request.Description.ToString(),
                Priority = Enum.Parse<TaskPriority>(request.Priority, true),
                DueDate = request.DueDate,
                UserId = request.AssignedToUserID,
                Status = TaskStatus.pending,
                CreatedDate = DateTime.UtcNow
            };

            await _taskRespository.CreateAsync(task);

            return new TaskResponseDto{
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Priority = task.Priority,
                Status = task.Status,
                CreatedDate = task.CreatedDate,
                DueDate = task.DueDate,
                UserId = task.UserId
            };
        }

        Task<TaskResponseDto> ITaskService.CreateTaskAsync(CreateTaskDto dto)
        {
            throw new NotImplementedException();
        }

        Task<bool> ITaskService.DeleteTaskAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<TaskResponseDto>> ITaskService.GetAllTasksAsync()
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

        Task<TaskResponseDto?> ITaskService.GetTaskByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<TaskResponseDto> ITaskService.UpdateTaskAsync(int id, UpdateTaskDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateTaskStatusAsync(int taskid, UpdateTaskStatusDto dto)
        {
            var task =await  _taskRespository.GetByIdAsync(taskid);
            if (task == null)
                throw new NotFoundExecption("Task not found.");
            var currentUserID = _currentUserService.UserId;
            if(task.UserId != currentUserID)
                throw new UnauthorizedAccessException("You are not authorized to update this task.");
            task.Status = (TaskStatus)dto.Status;
            await _taskRespository.UpdateAsync(task);
        }
    }
}
