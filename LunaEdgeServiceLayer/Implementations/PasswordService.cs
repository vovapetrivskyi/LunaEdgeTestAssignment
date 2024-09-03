﻿using LunaEdgeServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LunaEdgeServiceLayer.Implementations
{
	public class PasswordService : IPasswordService
	{
		public byte[] GenerateSalt()
		{
			using (var rng = new RNGCryptoServiceProvider())
			{
				byte[] salt = new byte[16];
				rng.GetBytes(salt);
				return salt;
			}
		}

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

		public bool VerifyPassword(string dbPassword, string password, byte[] salt)
		{
			return dbPassword == HashPassword(password, salt);
		}
	}
}
