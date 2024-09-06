using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunaEdgeServiceLayer.Interfaces
{
	/// <summary>
	/// Service for operations with password
	/// </summary>
	public interface IPasswordService
	{
		/// <summary>
		/// Generate 16 bytes salt
		/// </summary>
		/// <returns>Salt as byte array</returns>
		byte[] GenerateSalt();

		/// <summary>
		/// Hash password using salt
		/// </summary>
		/// <param name="password">String password</param>
		/// <param name="salt">Byte array salt</param>
		/// <returns>Hashed password string</returns>
		string HashPassword(string password, byte[] salt);

		/// <summary>
		/// Verify input password with hashed
		/// </summary>
		/// <param name="dbPassword">Saved in db password</param>
		/// <param name="password">Input password</param>
		/// <param name="salt">Saved in db salt</param>
		/// <returns>Is passwords equal</returns>
		bool VerifyPassword(string dbPassword, string password, byte[] salt);
	}
}
