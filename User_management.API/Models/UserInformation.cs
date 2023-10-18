using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace User_management.API.Models
{

    [Table("user_information")]
    public class UserInformation
    {
        [Key]
        public int InformationId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string? Firstname { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string? Lastname { get; set; }

        [MaxLength(80)]
        public string? Address { get; set; }

        [RegularExpression("/^[0-9]{5}$/")]
        public int? ZipCode { get; set; }

        [StringLength(30, MinimumLength = 4)]
        public string? Country { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }

        public DateTime RegisterDate { get; }

        public Role Role { get; set; }

        [ForeignKey("UserRegister")]
        public int UserId { get; set; }

        public virtual UserRegister UserRegister { get; set; }

        public UserInformation() { }
        public UserInformation(UserRegister user)
        {
            UserId = user.UserId;
            RegisterDate = DateTime.Now;
            Role = Role.User;
        }
    }
}
