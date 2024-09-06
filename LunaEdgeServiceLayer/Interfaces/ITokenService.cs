using LunaEdgeServiceLayer.Data.Models;

namespace LunaEdgeServiceLayer.Interfaces
{
	/// <summary>
	/// Service for generating JSON Web Tokens
	/// </summary>
	public interface ITokenService
	{
		/// <summary>
		/// Create token for user
		/// </summary>
		/// <param name="user">User</param>
		/// <returns>JWT</returns>
		string CreateToken(User user);
	}
}
