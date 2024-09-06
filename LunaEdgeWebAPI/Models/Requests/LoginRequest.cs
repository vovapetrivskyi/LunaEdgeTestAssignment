using System.ComponentModel.DataAnnotations;

namespace LunaEdgeWebAPI.Models.Requests
{
	/// <summary>
	/// Request for login into system
	/// </summary>
	public class LoginRequest
	{
		/// <summary>
		/// Username ot email
		/// </summary>
		[Required]
		public string Login { get; set; }

		/// <summary>
		/// Password (8 characters include 1 uppercase letter, 1 digit, 1 special character)
		/// </summary>
		[Required]
		public string Password { get; set; }
	}
}
