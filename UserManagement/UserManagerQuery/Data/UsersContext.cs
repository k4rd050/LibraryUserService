using Microsoft.EntityFrameworkCore;
using UserManagerQuery.Model;

namespace UserManagerQuery.Data
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options): base(options){}

        public DbSet<User> Users { get; set; }
    }
}
