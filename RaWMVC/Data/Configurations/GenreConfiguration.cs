using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic;
using RaWMVC.Data.Entities;
using System.Reflection.Emit;

namespace RaWMVC.Data.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.Property(c => c.GenreName)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(c => c.GenreDescription)
            .HasMaxLength(75);

            builder.Property(g => g.GenreId)
                    .HasColumnName("genreId");

            //builder.HasData(new Entities.Genre
            //{ genreId = Guid.Parse("06691ea9-d2cf-473b-a592-dbab69fe3ffe"), 
            //  genreName = "Action", 
            //  genreDescription = "Fast-paced plot with intense events." });

            //builder.HasData(new Entities.Genre
            //{ genreId = Guid.Parse("32a29cb8-b605-456d-82f2-76fb5564deee"), 
            //  genreName = "Adventure", 
            //  genreDescription = "Exciting journey or quest." });

        }
    }
}
