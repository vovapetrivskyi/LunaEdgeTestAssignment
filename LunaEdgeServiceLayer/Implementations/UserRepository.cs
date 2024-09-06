using LunaEdgeServiceLayer.Data.Models;
using LunaEdgeServiceLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LunaEdgeServiceLayer.Implementations
{
	/// <summary>
	/// Repository for operations with user
	/// </summary>
	public class UserRepository : RepositoryBase<User>, IUserRepository
	{
		public UserRepository(IUnitOfwork unitOfwork) : base(unitOfwork)
		{
		}

		/// <summary>
		/// Find user by uesrname or email
		/// </summary>
		/// <param name="login">Username or email</param>
		/// <returns>Existing user or null</returns>
		public async Task<User> FindByLoginAsync(string login)
		{
			return await dbSet.FirstOrDefaultAsync(e => e.Username == login || e.Email == login);
		}
	}
}
