using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Data.Entities;

namespace RaWMVC.Data.Configurations
{
    public class ReadingListStoryConfiguration : IEntityTypeConfiguration<ReadingListStory>
    {
        public void Configure(EntityTypeBuilder<ReadingListStory> builder)
        {
            builder.HasKey(rls => new { rls.ReadingListId, rls.StoryId });

            builder.HasOne(rls => rls.ReadingLists)
                .WithMany(rl => rl.ReadingListStories)
                .HasForeignKey(rls => rls.ReadingListId);

            builder.HasOne(rls => rls.Story)
                .WithMany(s => s.ReadingListStories)
                .HasForeignKey(rls => rls.StoryId);
        }
    }
}
