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

		[HttpGet]
		public async Task<IActionResult> GetAllTasks(TaskQueryParameters queryParameters) 
		{
			if (Guid.TryParse(HttpContext.User.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out Guid userId))
			{
				var tasks = await _taskService.GetTasks(queryParameters, userId);

				return Ok(tasks);
			}

			return BadRequest();
		}

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
