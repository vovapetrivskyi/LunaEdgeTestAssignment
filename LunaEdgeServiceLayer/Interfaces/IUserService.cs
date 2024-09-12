using LunaEdgeServiceLayer.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace LunaEdgeServiceLayer.Interfaces
{
	/// <summary>
	/// Service for operations with users
	/// </summary>
	public interface IUserService
	{
		/// <summary>
		/// Find user by username or email
		/// </summary>
		/// <param name="login">Username or email</param>
		/// <returns>Existing user or null</returns>
		Task<ActionResult<User>> SaveUser(User user);

		/// <summary>
		/// Add new user
		/// </summary>
		/// <param name="user">New user</param>
		/// <returns>Saved users</returns>
		Task<User> FindByLoginAsync(string login);
	}
}
