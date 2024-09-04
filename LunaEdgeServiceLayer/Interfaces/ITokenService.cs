using LunaEdgeServiceLayer.Data.Models;

namespace LunaEdgeServiceLayer.Interfaces
{
	public interface ITokenService
	{
		string CreateToken(User user);
	}
}
