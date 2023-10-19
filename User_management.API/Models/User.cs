using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using User_management.API.Services;

namespace User_management.API.Models
{
    [Table("user")]
    public class User
    {
        
        [Key] 
        public int UserId { get; private set; }

        [Required]
        [RegularExpression("[a-zA-Z0-9_]+")]
        [StringLength(20, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public Role Role { get; set; }

        public User() 
        {
        }

        public User(UserCreation newUser)
        {
            Username = newUser.Username;
            Email = newUser.Email;
            Password = PasswordHasherService.HashPassword(newUser.Password);
            Role = Role.User;
        }
    }
}
