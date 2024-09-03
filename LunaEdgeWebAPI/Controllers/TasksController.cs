using Microsoft.AspNetCore.Mvc;

namespace LunaEdgeWebAPI.Controllers
{
	public class TasksController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
