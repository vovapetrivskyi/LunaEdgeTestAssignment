using LunaEdgeServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace LunaEdgeServiceLayer.Implementations
{
	/// <summary>
	/// Service for operations with tasks
	/// </summary>
	public class TaskService : ITaskService
	{
		private readonly IUnitOfwork _unitOfWork;
		ITaskRepository _repository;

		public TaskService(IUnitOfwork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_repository = new TaskRepository(_unitOfWork);
		}

		/// <summary>
		/// Create new task
		/// </summary>
		/// <param name="task">New task</param>
		/// <returns>Saved task</returns>
		public async Task<Data.Models.Task> CreateNewTask(Data.Models.Task task)
		{
			return await _repository.Create(task);
		}

		/// <summary>
		/// Delete existing task
		/// </summary>
		/// <param name="taskId">Id of the task</param>
		/// <param name="userId">Id of the user</param>
		/// <returns>Delete result</returns>
		public async System.Threading.Tasks.Task<IActionResult> DeleteTask(Guid taskId, Guid userId)
		{
			return await _repository.DeleteTask(taskId, userId);
		}

		/// <summary>
		/// Find task by id
		/// </summary>
		/// <param name="taskId">Id of the task</param>
		/// <param name="userId">Id of the user</param>
		/// <returns>Task</returns>
		public async Task<Data.Models.Task> GetTaskById(Guid taskId, Guid userId)
		{
			return await _repository.GetTaskByIdAndUser(taskId, userId);
		}

		/// <summary>
		/// Get task of the authenticated user
		/// </summary>
		/// <param name="parameters">filter parameters</param>
		/// <param name="userId">Id of the user</param>
		/// <param name="sortBy">Property for sorting</param>
		/// <param name="sortDirection">Direction of sorting</param>
		/// <param name="page">Page of collection</param>
		/// <param name="pageSize">Tasks per page</param>
		/// <returns>List of tasks</returns>
		public async Task<IEnumerable<Data.Models.Task>> GetTasks(Data.Models.TaskQueryFilterParameters parameters, Guid userId, string sortBy, string sortDirection, int page, int pageSize)
		{
			var tasks = (await _repository.Get()).Where(t => t.UserId == userId);

			if (tasks == null)
			{
				return Enumerable.Empty<Data.Models.Task>();
			}
			else
			{
				//filtering
				if (parameters != null)
				{
					if (parameters.Title != string.Empty)
					{
						tasks = tasks.Where(t => t.Title.Contains(parameters.Title));
					}
					if (parameters.Description != string.Empty && parameters.Description != null)
					{
						tasks = tasks.Where(t => t.Description.Contains(parameters.Description));
					}
					if (parameters.DueDate != null)
					{
						tasks = tasks.Where(t => t.DueDate == parameters.DueDate);
					}
					if (parameters.Status != null)
					{
						tasks = tasks.Where(t => t.Status == parameters.Status);
					}
					if (parameters.Priority != null)
					{
						tasks = tasks.Where(t => t.Priority == parameters.Priority);
					}
					if (parameters.CreatedAt != null)
					{
						tasks = tasks.Where(t => t.CreatedAt == parameters.CreatedAt);
					}
					if (parameters.UpdatedAt != null)
					{
						tasks = tasks.Where(t => t.UpdatedAt == parameters.UpdatedAt);
					}
				}

				if (!string.IsNullOrWhiteSpace(sortBy))
				{
					//sorting
					tasks = sortDirection.ToLower() switch
					{
						"desc" => tasks.OrderByDescending(t => GetPropertyValue(t, sortBy)),
						_ => tasks.OrderBy(t => GetPropertyValue(t, sortBy)),
					};
				}

				// Pagination
				tasks = tasks
					.Skip((page - 1) * pageSize)
					.Take(pageSize)
					.ToList();

				return tasks;
			}
		}

		/// <summary>
		/// Update existing task
		/// </summary>
		/// <param name="task">Updated task</param>
		/// <param name="userId">Id of the user</param>
		/// <returns>Update result</returns>
		public async System.Threading.Tasks.Task<IActionResult> UpdateTask(Data.Models.Task task, Guid userId)
		{
			return await _repository.UpdateTask(task.Id, task, userId, nameof(task.CreatedAt));
		}

		/// <summary>
		/// /Helper method to get the property value dynamically by property name
		/// </summary>
		/// <param name="task">Task Object</param>
		/// <param name="propertyName">Sorting property name</param>
		/// <returns>Property value</returns>
		private object GetPropertyValue(Data.Models.Task task, string propertyName)
		{
			return typeof(Data.Models.Task).GetProperty(propertyName)?.GetValue(task, null) ?? 0;
		}
	}
}
