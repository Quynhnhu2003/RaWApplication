using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Data.Entities;

namespace RaWMVC.Data.Configurations
{
    public class ReplyConfiguration : IEntityTypeConfiguration<Reply>
    {
        public void Configure(EntityTypeBuilder<Reply> builder)
        {
            builder.HasKey(r => r.ReplyId);

            builder.Property(s => s.ReplyContent)
                .IsRequired()
                .HasMaxLength(300);

            // Thiết lập mối quan hệ với Post
            builder.HasOne(c => c.Post)
                .WithMany(p => p.Replies)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
