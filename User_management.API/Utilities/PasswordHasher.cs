using System.Security.Cryptography;
using System.Text;

namespace User_management.API.Utilities
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashValue = SHA256.HashData(messageBytes);
            return Convert.ToHexString(hashValue);
        }
    }
}
