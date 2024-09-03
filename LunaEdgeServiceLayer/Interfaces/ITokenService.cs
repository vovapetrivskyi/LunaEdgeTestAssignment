using LunaEdgeRepositoryLayer.Models;

namespace LunaEdgeServiceLayer.Interfaces
{
	public interface ITokenService
	{
		string CreateToken(User user);
	}
}
