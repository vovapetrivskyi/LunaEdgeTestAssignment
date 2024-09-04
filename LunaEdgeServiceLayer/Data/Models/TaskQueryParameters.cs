using LunaEdgeServiceLayer.Data.Models.Enums;

namespace LunaEdgeServiceLayer.Data.Models
{
	public class TaskQueryParameters
	{
		public string Title { get; set; } = string.Empty;
		public string? Description { get; set; } = null;
		public DateTime? DueDate { get; set; } = null;
		public TaskStatusEnum? Status { get; set; } = null;
		public TaskPriorityEnum? Priority { get; set; } = null;
		public DateTime? CreatedAt { get; set; } = null;
		public DateTime? UpdatedAt { get; set; } = null;
	}
}
