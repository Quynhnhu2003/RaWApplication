using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Data.Entities;

namespace RaWMVC.Data.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(r => r.ReportId);

            // Configure relationships
            builder.HasOne(r => r.Story)
                   .WithMany(s => s.Reports) // Ensure Story has a Reports collection
                   .HasForeignKey(r => r.StoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Configure required properties
            builder.Property(r => r.UserId)
                   .IsRequired();

            builder.Property(r => r.Reason)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(r => r.Description)
                   .HasMaxLength(500);

            builder.Property(r => r.AdminComments)
                   .HasMaxLength(500);

            builder.Property(r => r.IsReviewed)
                   .HasDefaultValue(false);

            builder.Property(r => r.IsApproved)
                   .HasDefaultValue(false);
        }
    }
}
