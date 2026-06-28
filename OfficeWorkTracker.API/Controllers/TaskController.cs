using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeWorkTracker.Application.Common;
using OfficeWorkTracker.Application.DTOs;
using OfficeWorkTracker.Application.DTOs.Task;
using OfficeWorkTracker.Application.Exception;
using OfficeWorkTracker.Application.Interfaces;
using OfficeWorkTracker.Domain.Constants;
using System.Security.Claims;

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
        public async Task<IActionResult> GetAllTasks([FromQuery] TaskFilterDto filter)
        {
            var result = await _taskService.GetFilteredTaskAsync(filter);
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

        [Authorize]
        [HttpGet("My")]
        public async Task<IActionResult> GetMyTask()
        {
            var userid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _taskService.GetTaskByIdAsync(userid);

            return Ok(result);
        }
        [Authorize(Roles = $"{Roles.Admin},{Roles.Manager}")]
        [HttpPost("assign")]
        public async Task<IActionResult> AssignTask(AssignTaskRequestDto request)
        {
            var result = await _taskService.AssignTaskAsync(request);
            return Ok(ApiResponseFactory.Success(result, "Task assigned successfully"));
        }
        [Authorize]
        [HttpPost("{id}/status")]
        public async Task<IActionResult> UpdateTaskStatus(int id, UpdateTaskStatusDto dto)
        {
            await _taskService.UpdateTaskStatusAsync(id, dto);
            return Ok(ApiResponseFactory.Success<Object>(null, "Task status updated successfully"));
        }

        [Authorize]
        [HttpPost("{taskId}/comment")]
        public async Task<IActionResult> AddComment(int taskId, AddCommentDto request)
        {
            await _taskService.AddCommentAsync(taskId, request);
            return Ok(ApiResponseFactory.Success<object>(null, "Comment added successfully"));
        }

        [HttpGet("{taskId}/History")]
        public async Task<IActionResult> GetTaskHistory(int taskId)
        {
            var result = await _taskService.GetTaskHistoryAsync(taskId);
            return Ok(ApiResponseFactory.Success(result, "Task history retrieved successfully"));
        }

    }
}
