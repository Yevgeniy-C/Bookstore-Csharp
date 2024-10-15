using System;
namespace Estore.BL.Auth
{
	public interface IEncrypt
	{
		string HashPassword(string password, string salt);

		string EncryptString(string text);

		string DecryptString(string? text);
	}
}

