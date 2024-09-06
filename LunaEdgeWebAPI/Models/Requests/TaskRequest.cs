using LunaEdgeServiceLayer.Data.Models.Enums;

namespace LunaEdgeWebAPI.Models.Requests
{
	/// <summary>
	/// Craeting new task request
	/// </summary>
	public class TaskRequest
	{
		/// <summary>
		/// Title of the task
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// Description of the task
		/// </summary>
		public string? Description { get; set; }
		/// <summary>
		/// Due date of the task, may be null
		/// </summary>
		public DateTime? DueDate { get; set; }
		/// <summary>
		/// Task status: Pending, In Progress, Completed
		/// </summary>
		public TaskStatusEnum Status { get; set; }
		/// <summary>
		/// Task priority: Low, Medium, High
		/// </summary>
		public TaskPriorityEnum Priority { get; set; }
	}
}
