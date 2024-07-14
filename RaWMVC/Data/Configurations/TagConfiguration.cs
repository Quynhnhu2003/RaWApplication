using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RaWMVC.Commons;
using RaWMVC.Data.Entities;

namespace RaW.MVC.Data.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder) 
        {
            builder.Property(t => t.tagName)
                .IsRequired()
                .HasMaxLength(Constants.MAXLENGTH_EntitiesName);
            builder.Property(t => t.tagDescription)
                .HasMaxLength(Constants.MAXLENGTH_EntitiesDescription);

            builder.HasData(new Tag
            {
                tagId = Guid.Parse("2ce6c267-d3fc-4820-8f04-6547696bab28"),
                tagName = "Boylove",
                tagDescription = "Romance between male characters."
            });
            builder.HasData(new Tag
            {
                tagId = Guid.Parse("e7196f16-0bf0-478f-8892-2ef83d09d14c"),
                tagName = "Girllove",
                tagDescription = "Romance between female characters."
            });
            builder.HasData(new Tag
            {
                tagId = Guid.Parse("acecd581-de79-498d-b4d6-c078a688e407"),
                tagName = "Albino",
                tagDescription = "Character with albinism."
            });
            builder.HasData(new Tag
            {
                tagId = Guid.Parse("48fa837c-6f38-4bf3-a52e-150d326f65d7"),
                tagName = "Pale",
                tagDescription = "Character with pale skin tone."
            });
            builder.HasData(new Tag
            {
                tagId = Guid.Parse("01e61df7-c444-4555-a4ad-6b6cd8cb182f"),
                tagName = "Dark brown skin tone",
                tagDescription = "Character with dark brown skin."
            });
        }
    }
}
