using System.ComponentModel.DataAnnotations;

namespace User_management.API.Models
{
    public class User
    {
        public int UserId { get; private set; }

        [Required]
        [RegularExpression("/^[a-zA-Z0-9_]+$/")]
        [StringLength(20, MinimumLength = 5)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; }

        public DateTime RegisterDate { get; }

        public Rol Rol { get; set; }

        public User(string userName, string email, string password)
        {
            UserName = userName;
            Email = email;
            Password = password;
            RegisterDate = DateTime.Now;
            Rol = Rol.User;
        }
    }
}
