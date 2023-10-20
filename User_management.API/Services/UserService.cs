using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using User_management.API.Models;

namespace User_management.API.Services
{
    public class UserService
    {

        private readonly UsersContext _usersContext;

        public UserService(UsersContext usersContext) 
        { 
            _usersContext = usersContext; 
        
        }

        public User GetUserFromClaims(ClaimsPrincipal claimsPrincipal)
        {
            var username = claimsPrincipal.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Name).Value;
            var userId = GetIdFromUsername(username);
            var user = GetUserFromUserId(userId);
            return user;
        }

        public List<UserInfo> GetAllUsers()
        {
            List<User> allUsers = _usersContext.User.ToList();

            // Filter sensitive data
            List<UserInfo> usersToReturn = allUsers.Select(u => new UserInfo(u)).ToList();

            return usersToReturn;
        }

        public dynamic GetUserFromUserId(int userId)
        {
            var user = _usersContext.User.FirstOrDefault(u => u.UserId == userId);
            return user;
        }
        public int GetIdFromUsername(string username)
        {
            var users = _usersContext.User;
            return users.FirstOrDefault(u => u.Username == username).UserId;
        }

        public bool UsernameExists(string username)
        {
            var existentUser = _usersContext.User.FirstOrDefault(u => u.Username == username);
            return existentUser != null;
        }

        public bool EmailExists(string email)
        {
            var existentUser = _usersContext.User.FirstOrDefault(u => u.Email == email);
            return existentUser != null;
        }

        public string GetRoleFromId(int id)
        {
            return _usersContext.User.First(u => u.UserId == id).Role.ToString();
        }
        public string GetUsernameFromId(int id)
        {
            return _usersContext.User.First(u => u.UserId == id).Username;
        }

        public string GenerateRandomString(int stringLength)
        {
            string charsAllowed = "ABCDEFGHIJKLMONOPQRSTUVWXYZabcdefghijklmonopqrstuvwxyz0123456789_";
            char[] randomChars = new char[stringLength];

            for (int i = 0; i < stringLength; i++)
            {
                randomChars[i] = charsAllowed[RandomNumberGenerator.GetInt32(0, charsAllowed.Length - 1)];
            }

            string randomString = new string(randomChars);
            return randomString;
        }
    }
}

