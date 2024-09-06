namespace LunaEdgeWebAPI.Models.Responses
{
	/// <summary>
	/// Response for successfully authenticated user
	/// </summary>
	public class AuthResponse
	{
		/// <summary>
		/// Name of user of email
		/// </summary>
		public string Username { get; set; }
		/// <summary>
		/// JWT Token
		/// </summary>
		public string Token { get; set; }
	}
}
