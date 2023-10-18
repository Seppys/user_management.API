using Microsoft.EntityFrameworkCore;

namespace User_management.API.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
        }

        public DbSet<UserRegister> UserRegister { get; set; }
        public DbSet<UserInformation> UserInformation { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
