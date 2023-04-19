using System.Security.Cryptography;
using System.Text;

namespace BL.Hashing
{
    public class PasswordHash
    {
        public static string Hashed_Password(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

    }
}
