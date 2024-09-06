using LunaEdgeServiceLayer.Data.Models.Enums;

namespace LunaEdgeServiceLayer.Data.Models
{
	/// <summary>
	/// Parameters for filtering tasks
	/// </summary>
	public class TaskQueryFilterParameters
	{
		/// <summary>
		/// Title of the task, default empty string
		/// </summary>
		public string Title { get; set; } = string.Empty;
		/// <summary>
		/// Description of the task, default null
		/// </summary>
		public string? Description { get; set; } = null;
		/// <summary>
		/// Due date of the task, may be null
		/// </summary>
		public DateTime? DueDate { get; set; } = null;
		/// <summary>
		/// Task status: Pending, In Progress, Completed. Default null
		/// </summary>
		public TaskStatusEnum? Status { get; set; } = null;
		/// <summary>
		/// Task priority: Low, Medium, High. Default null
		/// </summary>
		public TaskPriorityEnum? Priority { get; set; } = null;
		/// <summary>
		/// Task creation date, default null
		/// </summary>
		public DateTime? CreatedAt { get; set; } = null;
		/// <summary>
		/// Task update date, default null
		/// </summary>
		public DateTime? UpdatedAt { get; set; } = null;
	}
}
