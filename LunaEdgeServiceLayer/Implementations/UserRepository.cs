using LunaEdgeServiceLayer.Data.Models;
using LunaEdgeServiceLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LunaEdgeServiceLayer.Implementations
{
	public class UserRepository : RepositoryBase<User>, IUserRepository
	{
		public UserRepository(IUnitOfwork unitOfwork) : base(unitOfwork)
		{
		}

		public async Task<User> FindByLoginAsync(string login)
		{
			return await dbSet.FirstOrDefaultAsync(e => e.Username == login || e.Email == login);
		}
	}
}
