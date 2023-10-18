using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using User_management.API.Services;

namespace User_management.API.Models
{
    [Table("user")]
    public class UserRegister
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
        [MinLength(5)]
        public string Password { get; set; }

        public UserRegister() 
        {
        }
    }
}
