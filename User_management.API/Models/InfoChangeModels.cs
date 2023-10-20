using System.ComponentModel.DataAnnotations;

namespace User_management.API.Models
{
    public class InfoChangeModels
    {
        public class ChangePasswordModel
        {
            [Required]
            [MinLength(5)]
            public string CurrentPassword { get; set; }

            [Required]
            [MinLength(5)]
            public string NewPassword { get; set; }
        }

        public class ChangeUsernameModel
        {
            [Required]
            [RegularExpression("[a-zA-Z0-9_]+")]
            [StringLength(20, MinimumLength = 5)]
            public string CurrentUsername { get; set; }

            [Required]
            [RegularExpression("[a-zA-Z0-9_]+")]
            [StringLength(20, MinimumLength = 5)]
            public string NewUsername { get; set; }
        }

        public class ChangeEmailModel
        {
            [Required]
            [EmailAddress]
            public string CurrentEmail { get; set; }

            [Required]
            [EmailAddress]
            public string NewEmail { get; set; }
        }
    }
}
