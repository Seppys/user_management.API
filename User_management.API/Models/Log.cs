using System.ComponentModel.DataAnnotations.Schema;

namespace User_management.API.Models
{
    [Table("log")]
    public class Log
    {
        public int LogId { get; private set; }
        public string Description { get; set;} = string.Empty;
        public DateTime LodDate { get; set; } = DateTime.Now;

        [ForeignKey("User")]
        public int UserId { get; private set; }

        public virtual User User { get; set; }

        public Log() { }
    }
}
