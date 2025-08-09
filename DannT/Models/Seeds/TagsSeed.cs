using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DannT.Models.Seeds
{
    public class TagsSeed :IEntityTypeConfiguration<Tag>
    {
     

        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasData(
           new Tag { Id=1 ,Name = "Work" },
           new Tag { Id =2 ,Name = "Study" },
           new Tag { Id =3 ,Name = "Family" },
           new Tag { Id =4 ,Name = "Personal" },
           new Tag { Id = 5,Name = "Social" },
           new Tag { Id = 6,Name = "Economy" },
           new Tag { Id = 7,Name = "Health" },
           new Tag { Id = 8,Name = "Travel" }
           );

        }
    }
}
