using LunaEdgeServiceLayer.Implementations;

namespace LunaEdgeServiceLayer.Tests
{
	public class PasswordServiceTests
	{
		const string SaltString = "uAuJaqotbHqxpJtvxI8Y5A==";

		const string HashedDbPassword = "uAuJaqotbHqxpJtvxI8Y5Obl0ceOtA+OWF9r83+mlxCyD0t2uEaWADNxETb9Yt/q";

		[Fact]
		public void HashPassword_PasswordsEqual_Test()
		{
			var passwordService = new PasswordService();
						
			string hashedPassword = passwordService.HashPassword("password", Convert.FromBase64String(SaltString));

			Assert.Equal(HashedDbPassword, hashedPassword);
		}

		[Fact]
		public void HashPassword_PasswordsNotEqual_Test()
		{
			var passwordService = new PasswordService();

			string hashedPassword = passwordService.HashPassword("123", Convert.FromBase64String(SaltString));

			Assert.NotEqual(HashedDbPassword, hashedPassword);
		}

		[Fact]
		public void VerifyPassword_PasswordsEqual_Test()
		{
			var passwordService = new PasswordService();

			string password = "password";

			Assert.True(passwordService.VerifyPassword(HashedDbPassword, password, Convert.FromBase64String(SaltString)));
		}

		[Fact]
		public void VerifyPassword_PasswordsNotEqual_Test()
		{
			var passwordService = new PasswordService();

			string password = "123";

			Assert.False(passwordService.VerifyPassword(HashedDbPassword, password, Convert.FromBase64String(SaltString)));
		}
	}
}