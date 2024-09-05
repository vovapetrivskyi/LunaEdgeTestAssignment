using Microsoft.AspNetCore.Mvc;

namespace LunaEdgeServiceLayer.Interfaces
{
	public interface ITaskRepository : IRepository<Data.Models.Task>
	{
		Task<Data.Models.Task> GetTaskByIdAndUser(Guid taskId, Guid userId);
		Task<bool> UserHasTask(Guid taskId, Guid userId);
		Task<IActionResult> DeleteTask(Guid taskId, Guid userId);

		Task<IActionResult> UpdateTask(Guid taskId, Data.Models.Task task, Guid userId, params string[] ignoredProperties);
	}
}
