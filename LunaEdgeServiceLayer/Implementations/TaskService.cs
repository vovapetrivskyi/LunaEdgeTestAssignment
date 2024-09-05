using LunaEdgeServiceLayer.Data.Models;
using LunaEdgeServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public async System.Threading.Tasks.Task<IActionResult> DeleteTask(Guid taskId)
		{
			return await _repository.Delete(taskId);
		}

		public async Task<Data.Models.Task> GetTaskById(Guid taskId, Guid userGuid)
		{
			var task = await _repository.GetById(taskId);

			return task != null && task.UserId == userGuid ? task : null;
		}

		public async Task<IEnumerable<Data.Models.Task>> GetTasks(Data.Models.TaskQueryParameters parameters, Guid userGuid)
		{
			var tasks = await _repository.Get();

			if (tasks == null) 
			{
				return Enumerable.Empty<Data.Models.Task>();
			}
			else
			{
				tasks = tasks.Where(task => task.UserId == userGuid);

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

				return tasks;
			}
		}

		public async System.Threading.Tasks.Task<IActionResult> UpdateTask(Data.Models.Task task)
		{
			return await _repository.Update(task.Id, task, nameof(task.CreatedAt));
		}
	}
}
