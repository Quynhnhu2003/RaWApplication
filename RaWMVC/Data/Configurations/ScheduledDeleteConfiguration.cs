using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Data.Entities;

namespace RaWMVC.Data.Configurations
{
    public class ScheduledDeleteConfiguration : IEntityTypeConfiguration<ScheduledDelete>
    {
        public void Configure(EntityTypeBuilder<ScheduledDelete> builder)
        {
            builder.HasKey(r => r.ScheduledDeleteId);

        }
    }
}
