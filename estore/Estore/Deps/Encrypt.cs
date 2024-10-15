using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.DataProtection;

namespace Estore.Deps
{
	public class Encrypt : Estore.BL.Auth.IEncrypt
	{
        private readonly IDataProtector dataProtector;
        private const string EncryptionKey = "Здесь мы указываем ключ";

		public Encrypt(IDataProtectionProvider dataProtectionProvider)
		{
            this.dataProtector = dataProtectionProvider.CreateProtector(EncryptionKey);
		}

        public string HashPassword(string password, string salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                System.Text.Encoding.ASCII.GetBytes(salt),
                KeyDerivationPrf.HMACSHA512,
                5000,
                64
                ));
        }

        public string EncryptString(string text)
        {
            return dataProtector.Protect(text);
        }

        public string DecryptString(string? text)
        {
            if (String.IsNullOrEmpty(text))
                return "";
            return dataProtector.Unprotect(text) ?? "";
        }        
    }
}

