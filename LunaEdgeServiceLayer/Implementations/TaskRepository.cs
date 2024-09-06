using LunaEdgeServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LunaEdgeServiceLayer.Implementations
{
	/// <summary>
	/// Repository for operations with tasks
	/// </summary>
	public class TaskRepository : RepositoryBase<Data.Models.Task>, ITaskRepository
	{
		public TaskRepository(IUnitOfwork unitOfwork) : base(unitOfwork)
		{
		}

		/// <summary>
		/// Get current user task
		/// </summary>
		/// <param name="taskId">Id of the task</param>
		/// <param name="userId">Id of the user</param>
		/// <returns>Task</returns>
		public async Task<Data.Models.Task> GetTaskByIdAndUser(Guid taskId, Guid userId)
		{
			return await dbSet.FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);
		}

		/// <summary>
		/// Check if task assigned to user
		/// </summary>
		/// <param name="taskId">Id of the task</param>
		/// <param name="userId">Id of the user</param>
		/// <returns>True if task assigned to user, otherwise false</returns>
		public async Task<bool> UserHasTask(Guid taskId, Guid userId)
		{
			return await dbSet.AnyAsync(t => t.Id == taskId && t.UserId == userId);
		}

		/// <summary>
		/// Update given task
		/// </summary>
		/// <param name="taskId">Id of the task</param>
		/// <param name="task">Updated task</param>
		/// <param name="userId">Id of the user</param>
		/// <param name="ignoredProperties">Properties that should be ignored in update</param>
		/// <returns>Updation result</returns>
		public async Task<IActionResult> UpdateTask(Guid taskId, Data.Models.Task task, Guid userId, params string[] ignoredProperties)
		{
			if (await UserHasTask(taskId, userId))
			{
				return await Update(task.Id, task, nameof(task.CreatedAt));
			}

			return NotFound();
		}

		/// <summary>
		/// Delete task
		/// </summary>
		/// <param name="taskId">Id of the task</param>
		/// <param name="userId">Id of the user</param>
		/// <returns></returns>
		public async Task<IActionResult> DeleteTask(Guid taskId, Guid userId)
		{
			if (await UserHasTask(taskId, userId)) 
			{
				return await Delete(taskId);
			}

			return NotFound();
		}		
	}
}
