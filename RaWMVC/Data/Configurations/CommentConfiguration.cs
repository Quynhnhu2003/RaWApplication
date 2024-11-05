using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaWMVC.Commons;
using RaWMVC.Data.Entities;

namespace RaWMVC.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(r => r.CommentId);

            builder.Property(s => s.CommentContent)
                .IsRequired()
                .HasMaxLength(300);
        }
    }
}
