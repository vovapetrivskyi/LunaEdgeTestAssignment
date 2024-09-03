using LunaEdgeRepositoryLayer.Models;
using LunaEdgeServiceLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LunaEdgeServiceLayer.Implementations
{
	public class UserService : IUserService
	{
		public Task<User> FindByLoginAsync(string login)
		{
			throw new NotImplementedException();
		}

		public Task<IActionResult> SaveUser(User user)
		{
			throw new NotImplementedException();
		}
	}
}
