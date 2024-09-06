using LunaEdgeServiceLayer.Data.Models;

namespace LunaEdgeServiceLayer.Interfaces
{
	/// <summary>
	/// Repository for operations with user
	/// </summary>
	public interface IUserRepository : IRepository<User>
	{
		/// <summary>
		/// Find user by uesrname or email
		/// </summary>
		/// <param name="login">Username or email</param>
		/// <returns>Existing user or null</returns>
		Task<User> FindByLoginAsync(string login);
	}
}
