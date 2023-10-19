using System.Security.Cryptography;
using System.Text;

namespace User_management.API.Services
{
    public static class PasswordHasherService
    {
        public static string HashPassword(string password)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashValue = SHA256.HashData(messageBytes);
            return Convert.ToHexString(hashValue);
        }
    }
}
