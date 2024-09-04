using LunaEdgeServiceLayer.Data.Models;

namespace LunaEdgeServiceLayer.Interfaces
{
	public interface IUserRepository : IRepository<User>
	{
		Task<User> FindByLoginAsync(string login);
	}
}
