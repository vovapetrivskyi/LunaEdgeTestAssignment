using LunaEdgeServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LunaEdgeServiceLayer.Implementations
{
	public class TaskRepository : RepositoryBase<Data.Models.Task>, ITaskRepository
	{
		public TaskRepository(IUnitOfwork unitOfwork) : base(unitOfwork)
		{
		}

		public async Task<Data.Models.Task> GetTaskByIdAndUser(Guid taskId, Guid userId)
		{
			return await dbSet.FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);
		}

		public async Task<bool> UserHasTask(Guid taskId, Guid userId)
		{
			return await dbSet.AnyAsync(t => t.Id == taskId && t.UserId == userId);
		}

		public async Task<IActionResult> UpdateTask(Guid taskId, Data.Models.Task task, Guid userId, params string[] ignoredProperties)
		{
			if (await UserHasTask(taskId, userId))
			{
				return await Update(task.Id, task, nameof(task.CreatedAt));
			}

			return NotFound();
		}

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
