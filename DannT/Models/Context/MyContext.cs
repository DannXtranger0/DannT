using Microsoft.EntityFrameworkCore;

namespace DannT.Models.Context
{
    public class MyContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Models.Task> Tasks{ get; set; }
        public MyContext(DbContextOptions<MyContext> options) :base(options)
        {
            
        }

    }
}
