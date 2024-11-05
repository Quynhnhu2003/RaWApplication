using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaWMVC.Commons;
using RaWMVC.Data.Entities;

namespace RaWMVC.Data.Configurations
{
    public class ChapterConfiguration : IEntityTypeConfiguration<Chapter>
    {
        public void Configure(EntityTypeBuilder<Chapter> builder)
        {
            builder.Property(c => c.ChapterTitle)
                .IsRequired()
                .HasMaxLength(Constants.MAXLENGTH_EntitiesName);

            // Khóa chính
            builder.HasKey(c => c.ChapterId);

            // Thiết lập mối quan hệ
            builder.HasOne(c => c.Story)
              .WithMany(s => s.Chapters)
              .HasForeignKey(c => c.StoryId);

        }
    }
}
