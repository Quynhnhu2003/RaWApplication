using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Data.Entities;

namespace RaWMVC.Data.Configurations
{
    public class ReadingListsConfiguration : IEntityTypeConfiguration<ReadingList>
    {
        public void Configure(EntityTypeBuilder<ReadingList> builder)
        {
            builder.HasKey(r => r.ReadingListsId);

            builder.HasMany(r => r.ReadingListStories)
                .WithOne(r => r.ReadingLists)
                .HasForeignKey(r => r.ReadingListId);

        }
    }
}
