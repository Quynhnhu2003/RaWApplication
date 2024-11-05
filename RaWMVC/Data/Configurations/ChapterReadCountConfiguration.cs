using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaWMVC.Data.Entities;

namespace RaWMVC.Data.Configurations
{
    public class ChapterReadCountConfiguration : IEntityTypeConfiguration<ChapterReadCount>
    {
        public void Configure(EntityTypeBuilder<ChapterReadCount> builder)
        {
            // Khóa chính
            builder.HasKey(c => c.ChapterReadId);

        }
    }
}
