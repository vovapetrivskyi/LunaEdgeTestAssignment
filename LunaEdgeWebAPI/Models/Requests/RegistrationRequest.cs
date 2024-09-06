using System.ComponentModel.DataAnnotations;

namespace LunaEdgeWebAPI.Models.Requests
{
	/// <summary>
	/// Register new user request
	/// </summary>
	public class RegistrationRequest
	{
		/// <summary>
		/// Name of the user
		/// </summary>
		[Required]
		public string Username { get; set; }

		/// <summary>
		/// User's email address
		/// </summary>
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		/// <summary>
		/// Password
		/// </summary>
		[Required]
		[RegularExpression("^(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8}$", 
			ErrorMessage = "Password must consist of 8 characters and include at least 1 number and 1 uppercase letter and at least 1 special character")]
		public string Password { get; set; }
	}
}
