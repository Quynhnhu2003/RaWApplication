using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaWMVC.Commons;
using RaWMVC.Data.Entities;
using System.Reflection.Emit;

namespace RaWMVC.Data.Configurations
{
    public class LibraryConfiguration : IEntityTypeConfiguration<Library>
    {
        public void Configure(EntityTypeBuilder<Library> builder)
        {
            builder.HasKey(l => l.Id);

            // Relationships
            builder.HasOne(l => l.Story)
               .WithMany(s => s.Libraries)
               .HasForeignKey(l => l.StoryId)
               .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(l => l.UserId);

        }
    }
}
