using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace User_management.API.Models
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }


        [Required]
        public string Password { get; set; }
    }
}
