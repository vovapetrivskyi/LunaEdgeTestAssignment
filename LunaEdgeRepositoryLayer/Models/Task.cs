using LunaEdgeRepositoryLayer.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunaEdgeRepositoryLayer.Models
{
	public class Task
	{
        public Guid Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatusEnum Status { get; set; }
        public TaskPriorityEnum Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
		public Guid UserId { get; set; }
		public User User { get; set; }
    }
}
