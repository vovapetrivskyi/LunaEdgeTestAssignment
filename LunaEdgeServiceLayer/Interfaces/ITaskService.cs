using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunaEdgeServiceLayer.Interfaces
{
	public interface ITaskService
	{
		Task<ActionResult<Data.Models.Task>> CreateNewTask(Data.Models.Task task);
		Task<IEnumerable<Data.Models.Task>> GetTasks(Data.Models.TaskQueryParameters parameters);
		Task<ActionResult<Data.Models.Task>> GetTaskById(Guid taskId);
		Task<IActionResult> UpdateTask(Data.Models.Task task);
		Task<IActionResult> DeleteTask(Guid taskId);
	}
}
