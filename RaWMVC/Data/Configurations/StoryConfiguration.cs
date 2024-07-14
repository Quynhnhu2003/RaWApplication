using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaWMVC.Data.Entities;

namespace RaW.MVC.Data.Configurations
{
    public class StoryConfiguration : IEntityTypeConfiguration<Story>
    {
        public void Configure(EntityTypeBuilder<Story> builder)
        {
            builder.Property(s => s.storyTitle)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.storyDescription)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(s => s.coverImage)
                .HasMaxLength(100);

            builder.Property(s => s.publishDate)
                .IsRequired()
                .HasMaxLength(10);

            builder.HasData(new Story
            {
                storyId = Guid.NewGuid(),
                storyTitle = "THE GUARDIANS OF LORE",
                storyDescription = "While drawing in class at Stagwood School, 12-year old Cal sees a frog staring at him through the window. Stranger than that, is the fact that this frog happens to be wearing glasses.",
                genreId = Guid.Parse("32a29cb8-b605-456d-82f2-76fb5564deee"),
                statusId = Guid.Parse("2f85b983-e349-4a46-b829-a77cd30be50f"),
                tagId = Guid.Parse("acecd581-de79-498d-b4d6-c078a688e407"),
                publishDate = DateTime.Now,
            });

            builder.HasData(new Story
            {
                storyId = Guid.NewGuid(),
                storyTitle = "WHEN DO HIPPOS PLAY?",
                storyDescription = "Do you know what the Hippos of Africa do long after the Elephants and Rhinos have gone to sleep? This rhythmical story will teach kids about the habits of Hippos at night. Full of true facts and fun rhymes, kids stories don’t get more fun than this! ",
                genreId = Guid.Parse("32a29cb8-b605-456d-82f2-76fb5564deee"),
                statusId = Guid.Parse("2f85b983-e349-4a46-b829-a77cd30be50f"),
                tagId = Guid.Parse("48fa837c-6f38-4bf3-a52e-150d326f65d7"),
                publishDate = DateTime.Now,
            });
        }
    }
}
