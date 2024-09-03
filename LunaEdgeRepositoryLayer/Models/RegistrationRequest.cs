using System.ComponentModel.DataAnnotations;

namespace LunaEdgeRepositoryLayer.Models
{
	public class RegistrationRequest
	{
		[Required]
		public string Username { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
