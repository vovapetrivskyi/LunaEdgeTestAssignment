using Microsoft.AspNetCore.Mvc;

namespace LunaEdgeServiceLayer.Interfaces
{
	public interface ITaskService
	{
		Task<Data.Models.Task> CreateNewTask(Data.Models.Task task);
		Task<IEnumerable<Data.Models.Task>> GetTasks(Data.Models.TaskQueryParameters parameters, Guid userId, string sortBy, string sortDirection, int page, int pageSize);
		Task<Data.Models.Task> GetTaskById(Guid taskId, Guid userId);
		Task<IActionResult> UpdateTask(Data.Models.Task task, Guid userId);
		Task<IActionResult> DeleteTask(Guid taskId, Guid userId);
	}
}
