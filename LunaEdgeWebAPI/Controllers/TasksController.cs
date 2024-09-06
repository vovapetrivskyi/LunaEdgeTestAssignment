using LunaEdgeServiceLayer.Data.Models;
using LunaEdgeServiceLayer.Interfaces;
using LunaEdgeWebAPI.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LunaEdgeWebAPI.Controllers
{
	[Authorize]
	[ApiController]
	[Route("tasks")]
	public class TasksController : Controller
	{
		private ITaskService _taskService;

		public TasksController(ITaskService taskService)
		{
			_taskService = taskService;
		}

		/// <summary>
		/// Get all task of the authenticated user
		/// </summary>
		/// <param name="queryParameters">Parameters for filtering</param>
		/// <param name="sortBy">Property for sorting, default null</param>
		/// <param name="sortDirection">Order of sorting, default ascending</param>
		/// <param name="page">Page for tasks view, default 1</param>
		/// <param name="pageSize">Number of tasks per page, default 10</param>
		/// <returns>User tasks</returns>
		[HttpGet]
		public async Task<IActionResult> GetAllTasks(TaskQueryFilterParameters queryParameters, 
			[FromQuery] string? sortBy = null,
			[FromQuery] string? sortDirection = "asc",
			[FromQuery] int page = 1,
			[FromQuery] int pageSize = 10) 
		{
			if (Guid.TryParse(HttpContext.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out Guid userId))
			{
				var tasks = await _taskService.GetTasks(queryParameters, userId, sortBy, sortDirection, page, pageSize);

				return Ok(tasks);
			}

			return BadRequest();
		}

		/// <summary>
		/// Get task for authenticated usr by Id
		/// </summary>
		/// <param name="id">Id of the task</param>
		/// <returns>Task</returns>
		[HttpGet("{id}")]
		public async Task<IActionResult> GetTaskById(string id)
		{
			if (Guid.TryParse(id, out Guid taskId) &&
				Guid.TryParse(HttpContext.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out Guid userId))
			{
				return Ok(await _taskService.GetTaskById(taskId, userId));
			}
			
			return BadRequest();
		}

		/// <summary>
		/// Create new task for the authenticated user
		/// </summary>
		/// <param name="newTask">Task creation DTO</param>
		/// <returns>Result of task creation</returns>
		[HttpPost]
		public async Task<IActionResult> CreateTask(TaskRequest newTask)
		{
			var userId = HttpContext.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

			if (Guid.TryParse(userId, out Guid userIdGuid))
			{
				LunaEdgeServiceLayer.Data.Models.Task task = new LunaEdgeServiceLayer.Data.Models.Task()
				{
					Id = Guid.NewGuid(),
					CreatedAt = DateTime.Now,
					UpdatedAt = DateTime.UtcNow,
					Description = newTask.Description,
					DueDate = newTask.DueDate,
					Priority = newTask.Priority,
					Status = newTask.Status,
					Title = newTask.Title,
					UserId = userIdGuid
				};

				return Ok(await _taskService.CreateNewTask(task));
			}

			return BadRequest();
		}

		/// <summary>
		/// Update authenticated user task by id
		/// </summary>
		/// <param name="id">Task Id for update</param>
		/// <param name="updatedTask">Task DTO</param>
		/// <returns>Update result</returns>
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateTask(string id, [FromBody] TaskRequest updatedTask)
		{
			if (Guid.TryParse(id, out Guid taskId) &&
				Guid.TryParse(HttpContext.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out Guid userId))
			{
				LunaEdgeServiceLayer.Data.Models.Task task = new LunaEdgeServiceLayer.Data.Models.Task()
				{
					Id = taskId,
					UpdatedAt = DateTime.UtcNow,
					Description = updatedTask.Description,
					DueDate = updatedTask.DueDate,
					Priority = updatedTask.Priority,
					Status = updatedTask.Status,
					Title = updatedTask.Title,
					UserId = userId
				};

				await _taskService.UpdateTask(task, userId);

				return Ok();
			}

			return BadRequest();
		}

		/// <summary>
		/// Delete authenticated user task by id
		/// </summary>
		/// <param name="id">Id of the task for delete</param>
		/// <returns>delete result</returns>
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTask(string id)
		{
			if (Guid.TryParse(id, out Guid taskId) &&
				Guid.TryParse(HttpContext.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out Guid userId))
			{
				return await _taskService.DeleteTask(taskId, userId);
			}

			return BadRequest();
		}
	}
}
