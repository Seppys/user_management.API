using System.ComponentModel.DataAnnotations;

namespace User_management.API.Models
{
    public class UserCreation
    {

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
    }
}
