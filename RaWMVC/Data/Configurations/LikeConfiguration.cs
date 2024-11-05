using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaWMVC.Data.Entities;

namespace RaWMVC.Data.Configurations
{
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasKey(l => l.LikeId);

            builder.HasOne<Chapter>()
                .WithMany(c => c.Likes)
                .HasForeignKey(l => l.ChapterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(l => l.UserId)
                .IsRequired();

            builder.Property(l => l.ChapterId)
                .IsRequired();
        }
    }
}
