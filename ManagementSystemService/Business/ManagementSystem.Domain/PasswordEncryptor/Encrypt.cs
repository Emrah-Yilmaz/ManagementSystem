using System.Security.Cryptography;
using System.Text;

namespace ManagementSystem.Domain.PasswordEncryptor
{
    public class Encrypt
    {
        public static string Encript(string password)
        {
            using var md5 = MD5.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }
    }
}
