using Microsoft.AspNetCore.Mvc;

namespace LunaEdgeServiceLayer.Interfaces
{
	/// <summary>
	/// Service for operations with tasks
	/// </summary>
	public interface ITaskService
	{
		/// <summary>
		/// Create new task
		/// </summary>
		/// <param name="task">New task</param>
		/// <returns>Saved task</returns>
		Task<Data.Models.Task> CreateNewTask(Data.Models.Task task);

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
		Task<IEnumerable<Data.Models.Task>> GetTasks(Data.Models.TaskQueryFilterParameters parameters, Guid userId, string sortBy, string sortDirection, int page, int pageSize);

		/// <summary>
		/// Find task by id
		/// </summary>
		/// <param name="taskId">Id of the task</param>
		/// <param name="userId">Id of the user</param>
		/// <returns>Task</returns>
		Task<Data.Models.Task> GetTaskById(Guid taskId, Guid userId);

		/// <summary>
		/// Update existing task
		/// </summary>
		/// <param name="task">Updated task</param>
		/// <param name="userId">Id of the user</param>
		/// <returns>Update result</returns>
		Task<IActionResult> UpdateTask(Data.Models.Task task, Guid userId);

		/// <summary>
		/// Delete existing task
		/// </summary>
		/// <param name="taskId">Id of the task</param>
		/// <param name="userId">Id of the user</param>
		/// <returns>Delete result</returns>
		Task<IActionResult> DeleteTask(Guid taskId, Guid userId);
	}
}
