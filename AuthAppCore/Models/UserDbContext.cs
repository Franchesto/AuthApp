using AuthAppCore.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthAppCore.Models
{
    public class UserDbContext : DbContext
    {   
        public DbSet<User> Users { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {            
        }

    }
}
