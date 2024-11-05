using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaWMVC.Data.Entities;

namespace RaWMVC.Data.Configurations
{
    public class FollowConfiguration : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
            builder.HasKey(l => l.Id);

            builder.HasIndex(l => l.FollowerId);

            builder.HasIndex(l => l.FolloweeId);
        }
    }
}
