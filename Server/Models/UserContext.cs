using Microsoft.EntityFrameworkCore;
using LolaOfficial.Shared;

namespace LolaOfficial.Server.Models{
    public class UserContext : DbContext {
        public UserContext (DbContextOptions<UserContext> options) : base (options) { }

        public DbSet<User> Users { get; set; }
    }
}
