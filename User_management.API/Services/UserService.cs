﻿using Microsoft.EntityFrameworkCore;
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

        public dynamic GetUserFromUserId(int userId)
        {
            var user = _usersContext.UserInformation.FirstOrDefault(u => u.UserId == userId);
            return user;
        }

        public int GetIdFromUsername(string username)
        {
            var users = _usersContext.UserRegister;
            return users.FirstOrDefault(u => u.Username == username).UserId;
        }

        public bool UsernameExists(string username)
        {
            var existentUser = _usersContext.UserRegister.FirstOrDefault(u => u.Username == username);
            return existentUser != null;
        }

        public bool EmailExists(string email)
        {
            var existentUser = _usersContext.UserRegister.FirstOrDefault(u => u.Email == email);
            return existentUser != null;
        }

        public string GetRoleFromId(int id)
        {
            return _usersContext.UserInformation.First(u => u.UserId == id).Role.ToString();
        }
        public string GetUsernameFromId(int id)
        {
            return _usersContext.UserInformation.First(u => u.UserId == id).UserRegister.Username;
        }
    }
}
