using LunaEdgeServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace LunaEdgeServiceLayer.Implementations
{
	public class TaskService : ITaskService
	{
		private readonly IUnitOfwork _unitOfWork;
		ITaskRepository _repository;

		public TaskService(IUnitOfwork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_repository = new TaskRepository(_unitOfWork);
		}

		public async Task<Data.Models.Task> CreateNewTask(Data.Models.Task task)
		{
			return await _repository.Create(task);
		}

		public async System.Threading.Tasks.Task<IActionResult> DeleteTask(Guid taskId, Guid userId)
		{
			return await _repository.DeleteTask(taskId, userId);
		}

		public async Task<Data.Models.Task> GetTaskById(Guid taskId, Guid userGuid)
		{
			return await _repository.GetTaskByIdAndUser(taskId, userGuid);
		}

		public async Task<IEnumerable<Data.Models.Task>> GetTasks(Data.Models.TaskQueryParameters parameters, Guid userGuid, string sortBy, string sortDirection, int page, int pageSize)
		{
			var tasks = (await _repository.Get()).Where(t => t.UserId == userGuid);

			if (tasks == null)
			{
				return Enumerable.Empty<Data.Models.Task>();
			}
			else
			{
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

		public async System.Threading.Tasks.Task<IActionResult> UpdateTask(Data.Models.Task task, Guid userId)
		{
			return await _repository.UpdateTask(task.Id, task, userId, nameof(task.CreatedAt));
		}

		/// <summary>
		/// /Helper method to get the property value dynamically by property name
		/// </summary>
		/// <param name="task">Task Object</param>
		/// <param name="propertyName">Sorting property name</param>
		/// <returns>Property value </returns>
		private object GetPropertyValue(Data.Models.Task task, string propertyName)
		{
			return typeof(Data.Models.Task).GetProperty(propertyName)?.GetValue(task, null) ?? 0;
		}
	}
}
