using LunaEdgeRepositoryLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace LunaEdgeServiceLayer.Interfaces
{
	public interface IUserService
	{
		Task<IActionResult> SaveUser(User user);
		Task<User> FindByLoginAsync(string login);
	}
}
