using LunaEdgeServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunaEdgeServiceLayer.Implementations
{
	public class TaskRepository : RepositoryBase<Data.Models.Task>, ITaskRepository
	{
		public TaskRepository(IUnitOfwork unitOfwork) : base(unitOfwork)
		{
		}
	}
}
