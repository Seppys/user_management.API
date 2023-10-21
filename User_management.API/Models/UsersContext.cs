using Microsoft.EntityFrameworkCore;

namespace User_management.API.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
    }
}
