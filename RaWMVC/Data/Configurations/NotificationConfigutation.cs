using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Data.Entities;
using RaWMVC.Commons;

namespace RaWMVC.Data.Configurations
{
    public class NotificationConfigutation : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(r => r.NotificationId);

            builder.Property(n => n.Message)
               .IsRequired()
               .HasMaxLength(Constants.MAXLENGTH_EntitiesDescription);
        }
    }
}
