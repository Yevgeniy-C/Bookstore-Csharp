using System.Security.Cryptography;

namespace EstoreTests.BlTests
{
	public class EncryptTests : Helpers.BaseTest
    {
        [SetUp]
        public void Setup()
        {
        }

        /*
        [Test]
        public void TestMsEncrypt() {
            Console.WriteLine("MS Result = " + this.encrypt.EncryptString("12"));

            Console.WriteLine("Ms Decrypted = " + this.encrypt.DecryptString("CfDJ8OwRd8MVgu9Gp_oFKFaQ1H1lXwEbH5iHdnJ1bTjQQE7ly1QQHVr9PKWmN5sdiTVn-TfQXY2Xu2C-FQYj7cJb26McXmDc1DsB42V3hu08wk34X4-PcRyRr5tICHLrYXNEEQ"));
            Console.WriteLine("Ms Decrypted = " + this.encrypt.DecryptString("CfDJ8OwRd8MVgu9Gp_oFKFaQ1H2DSy0yJIzt--f9MExp1yRXIOJe5vlOcp9ovgqsw-y6bhM8bhXInFNJTpdMxPk1NJ0sCzSy3z0LzMzw-sk3huHxPGOeCFxr6GRh7Evr_nSIMQ"));
            Console.WriteLine("Ms Decrypted = " + this.encrypt.DecryptString("CfDJ8OwRd8MVgu9Gp_oFKFaQ1H0glV00WuIl0sCFd2F0Nr3kX8k5byaFX6KtDngp3Hw35y4kJh5dXxmRUKdtpPkKbJb7z1vXLO4gb1O-3cNlG7eSfiHwiTvqxEjYJBdaaTcI5A"));
        }

        [Test]
        public void TestEncrypt() {
            using (Aes myAes = Aes.Create())
            {
                byte[] IV = { 12, 3, 2, 4, 5, 6, 7, 8, 9, 10, 11, 1, 13, 14, 15, 16};
                myAes.Key = System.Text.Encoding.UTF8.GetBytes("b14ca5898a4e4133bbce2ea2315a1916");
                myAes.IV = IV;

                var original = "89";
                byte[] encrypted = EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);

                string roundtrip = DecryptStringFromBytes_Aes(Convert.FromBase64String("h8FOR97DkUn89wLUAa+oZQ=="), myAes.Key, myAes.IV);
                Console.WriteLine(Convert.ToBase64String(encrypted));
                Console.WriteLine(original + " " + roundtrip); 
            }
        }
        */

        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string? plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}