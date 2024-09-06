using Microsoft.AspNetCore.Mvc;

namespace LunaEdgeServiceLayer.Interfaces
{
	/// <summary>
	/// Repository for operations with tasks
	/// </summary>
	public interface ITaskRepository : IRepository<Data.Models.Task>
	{
		/// <summary>
		/// Get current user task
		/// </summary>
		/// <param name="taskId">Id of the task</param>
		/// <param name="userId">Id of the user</param>
		/// <returns>Task</returns>
		Task<Data.Models.Task> GetTaskByIdAndUser(Guid taskId, Guid userId);

		/// <summary>
		/// Check if task assigned to user
		/// </summary>
		/// <param name="taskId">Id of the task</param>
		/// <param name="userId">Id of the user</param>
		/// <returns>True if task assigned to user, otherwise false</returns>
		Task<bool> UserHasTask(Guid taskId, Guid userId);

		/// <summary>
		/// Update given task
		/// </summary>
		/// <param name="taskId">Id of the task</param>
		/// <param name="task">Updated task</param>
		/// <param name="userId">Id of the user</param>
		/// <param name="ignoredProperties">Properties that should be ignored in update</param>
		/// <returns>Updation result</returns>
		Task<IActionResult> DeleteTask(Guid taskId, Guid userId);

		/// <summary>
		/// Delete task
		/// </summary>
		/// <param name="taskId">Id of the task</param>
		/// <param name="userId">Id of the user</param>
		/// <returns></returns>
		Task<IActionResult> UpdateTask(Guid taskId, Data.Models.Task task, Guid userId, params string[] ignoredProperties);
	}
}
