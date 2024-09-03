using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunaEdgeServiceLayer.Interfaces
{
	public interface IPasswordService
	{
		byte[] GenerateSalt();
		string HashPassword(string password, byte[] salt);
		bool VerifyPassword(string dbPassword, string password, byte[] salt);
	}
}
