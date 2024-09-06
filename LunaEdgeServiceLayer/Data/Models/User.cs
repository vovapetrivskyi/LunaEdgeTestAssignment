namespace LunaEdgeServiceLayer.Data.Models
{
	/// <summary>
	/// User model
	/// </summary>
	public class User
	{
		/// <summary>
		/// User id
		/// </summary>
		public Guid Id { get; set; }
		/// <summary>
		/// Name of user
		/// </summary>
		public string Username { get; set; }
		/// <summary>
		/// User's email
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		/// Hashed password
		/// </summary>
		public string PasswordHash { get; set; }
		/// <summary>
		/// Password salt
		/// </summary>
		public string PasswordSalt { get; set; }
		/// <summary>
		/// Date of creation
		/// </summary>
		public DateTime CreatedAt { get; set; }
		/// <summary>
		/// Date of upadte
		/// </summary>
		public DateTime UpdatedAt { get; set; }
		/// <summary>
		/// List of user's tasks
		/// </summary>
		public ICollection<Task> Tasks { get; set; } = new List<Models.Task>();
	}
}
