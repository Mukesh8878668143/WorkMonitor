using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeWorkTracker.Application.Common;
using OfficeWorkTracker.Application.DTOs.Task;
using OfficeWorkTracker.Application.Exception;
using OfficeWorkTracker.Application.Interfaces;
using OfficeWorkTracker.Domain.Constants;

namespace OfficeWorkTracker.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskDto dto)
        {
            var result = await _taskService.CreateTaskAsync(dto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var result = await _taskService.GetAllTasksAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var result =
                await _taskService.GetTaskByIdAsync(id);

            if (result == null)
                throw new NotFoundExecption("Task not found in Repository.");

            return Ok(ApiResponseFactory.Success(result, "Task retrieved successfully."));
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result =
                await _taskService.DeleteTaskAsync(id);

            if (!result)
                return NotFound();

            return Ok("Task deleted successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskDto dto)
        {
            var result =
                await _taskService.UpdateTaskAsync(id, dto);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
