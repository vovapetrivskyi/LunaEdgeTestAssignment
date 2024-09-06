using LunaEdgeServiceLayer.Data.Models.Enums;

namespace LunaEdgeServiceLayer.Data.Models
{
	/// <summary>
	/// Task model
	/// </summary>
	public class Task
	{
		/// <summary>
		/// Task id
		/// </summary>
		public Guid Id { get; set; }
		/// <summary>
		/// Task title
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// Task description, can be null
		/// </summary>
		public string? Description { get; set; }
		/// <summary>
		/// Task due date, can be null
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
		/// <summary>
		/// Task creation date
		/// </summary>
		public DateTime CreatedAt { get; set; }
		/// <summary>
		/// Task update date
		/// </summary>
		public DateTime UpdatedAt { get; set; }
		/// <summary>
		/// Id of user, task assignet to
		/// </summary>
		public Guid UserId { get; set; }
		public User User { get; set; }
	}
}
