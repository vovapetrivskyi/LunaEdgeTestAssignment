using LunaEdgeServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LunaEdgeServiceLayer.Implementations
{
	/// <summary>
	/// Service for operations with password
	/// </summary>
	public class PasswordService : IPasswordService
	{
		/// <summary>
		/// Generate 16 bytes salt
		/// </summary>
		/// <returns>Salt as byte array</returns>
		public byte[] GenerateSalt()
		{
			using (var rng = new RNGCryptoServiceProvider())
			{
				byte[] salt = new byte[16];
				rng.GetBytes(salt);
				return salt;
			}
		}

		/// <summary>
		/// Hash password using salt
		/// </summary>
		/// <param name="password">String password</param>
		/// <param name="salt">Byte array salt</param>
		/// <returns>Hashed password string</returns>
		public string HashPassword(string password, byte[] salt)
		{
			using (var sha256 = new SHA256Managed())
			{
				byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
				byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

				// Concatenate password and salt
				Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
				Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

				// Hash the concatenated password and salt
				byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

				// Concatenate the salt and hashed password for storage
				byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
				Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
				Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

				return Convert.ToBase64String(hashedPasswordWithSalt);
			}
		}

		/// <summary>
		/// Verify input password with hashed
		/// </summary>
		/// <param name="dbPassword">Saved in db password</param>
		/// <param name="password">Input password</param>
		/// <param name="salt">Saved in db salt</param>
		/// <returns>Is passwords equal</returns>
		public bool VerifyPassword(string dbPassword, string password, byte[] salt)
		{
			return dbPassword == HashPassword(password, salt);
		}
	}
}
