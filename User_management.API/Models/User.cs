using System.ComponentModel.DataAnnotations;
using User_management.API.Utilities;

namespace User_management.API.Models
{
    public class User
    {
        public int UserId { get; private set; }

        [Required]
        [RegularExpression("[a-zA-Z0-9_]+")]
        [StringLength(20, MinimumLength = 5)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime RegisterDate { get; }

        private Rol Rol { get; set; }

        public User() {}

        public User(string userName, string email, string password)
        {
            UserName = userName;
            Email = email;
            Password = PasswordHasher.HashPassword(password);
            RegisterDate = DateTime.Now;
            Rol = Rol.User;
        }
    }
}
