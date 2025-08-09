using DannT.Models.Seeds;
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
            //En Task se guarda un campo con Enum, se guardará con su valor en string, pero por dentro 
            //se compara con valores enteros
            modelBuilder.Entity<Task>()
                .Property(t => t.Status)
                .HasConversion<string>();
            
            modelBuilder.ApplyConfiguration(new TagsSeed());
        }   

        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }

    }
}
