using Microsoft.EntityFrameworkCore;

namespace DannT.Models.Context
{
    public class MyContext : DbContext
    {
       
        public MyContext(DbContextOptions<MyContext> options) :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
        }   

        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }

    }
}
