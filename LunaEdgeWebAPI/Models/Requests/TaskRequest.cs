using LunaEdgeServiceLayer.Data.Models.Enums;

namespace LunaEdgeWebAPI.Models.Requests
{
	public class TaskRequest
	{
		public string Title { get; set; }
		public string? Description { get; set; }
		public DateTime? DueDate { get; set; }
		public TaskStatusEnum Status { get; set; }
		public TaskPriorityEnum Priority { get; set; }
	}
}
