using LunaEdgeServiceLayer.Data;
using System.ComponentModel.DataAnnotations;

namespace LunaEdgeWebAPI.Models.Requests
{
	public class RegistrationRequest
	{
		[Required]
		public string Username { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[RegularExpression("^(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8}$", 
			ErrorMessage = "Password must consist of 8 characters and include at least 1 number and 1 uppercase letter and at least 1 special character")]
		public string Password { get; set; }
	}
}
