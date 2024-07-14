using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaW.MVC.Commons;
using RaWMVC.Data.Entities;

namespace RaW.MVC.Data.Configurations
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.Property(s => s.statusName)
                .IsRequired()
                .HasMaxLength(Constants.MAXLENGTH_EntitiesName);

            builder.Property(s => s.statusDescription)
                .HasMaxLength(Constants.MAXLENGTH_EntitiesDescription);

            builder.HasData(new Status
            {
                statusId = Guid.Parse("2f85b983-e349-4a46-b829-a77cd30be50f"),
                statusName = "Ongoing",
                statusDescription = "Fast-paced plot with intense events.",
            });
            builder.HasData(new Status
            {
                statusId = Guid.Parse("bf3a1f0a-cb15-43f4-a270-30bf97762513"),
                statusName = "Completed",
                statusDescription = "Exciting journey or quest."
            });

            //throw new NotImplementedException();
        }
    }
}
