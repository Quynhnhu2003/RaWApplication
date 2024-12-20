﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RaW.MVC.Data;

#nullable disable

namespace RaWMVC.Data.Migrations
{
    [DbContext(typeof(RaWDbContext))]
    [Migration("20240712105226_Add_Position_To_All_Table")]
    partial class Add_Position_To_All_Table
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RaW.MVC.Data.Entities.Genre", b =>
                {
                    b.Property<Guid>("genreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<string>("genreDescription")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("genreName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("genreId");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            genreId = new Guid("06691ea9-d2cf-473b-a592-dbab69fe3ffe"),
                            Position = 0,
                            genreDescription = "Fast-paced plot with intense events.",
                            genreName = "Action"
                        },
                        new
                        {
                            genreId = new Guid("32a29cb8-b605-456d-82f2-76fb5564deee"),
                            Position = 0,
                            genreDescription = "Exciting journey or quest.",
                            genreName = "Adventure"
                        });
                });

            modelBuilder.Entity("RaW.MVC.Data.Entities.Status", b =>
                {
                    b.Property<Guid>("statusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<string>("statusDescription")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("statusName")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.HasKey("statusId");

                    b.ToTable("Status");

                    b.HasData(
                        new
                        {
                            statusId = new Guid("2f85b983-e349-4a46-b829-a77cd30be50f"),
                            Position = 0,
                            statusDescription = "Fast-paced plot with intense events.",
                            statusName = "Ongoing"
                        },
                        new
                        {
                            statusId = new Guid("bf3a1f0a-cb15-43f4-a270-30bf97762513"),
                            Position = 0,
                            statusDescription = "Exciting journey or quest.",
                            statusName = "Completed"
                        });
                });

            modelBuilder.Entity("RaW.MVC.Data.Entities.Story", b =>
                {
                    b.Property<Guid>("storyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<string>("coverImage")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("genreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("publishDate")
                        .HasMaxLength(10)
                        .HasColumnType("datetime2");

                    b.Property<Guid>("statusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("storyDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("storyTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("tagId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("storyId");

                    b.HasIndex("genreId");

                    b.HasIndex("statusId");

                    b.HasIndex("tagId");

                    b.ToTable("Stories");

                    b.HasData(
                        new
                        {
                            storyId = new Guid("15d1df89-fe11-45e5-88c9-2a2b4ec0d171"),
                            Position = 0,
                            genreId = new Guid("32a29cb8-b605-456d-82f2-76fb5564deee"),
                            publishDate = new DateTime(2024, 7, 12, 17, 52, 24, 842, DateTimeKind.Local).AddTicks(4657),
                            statusId = new Guid("2f85b983-e349-4a46-b829-a77cd30be50f"),
                            storyDescription = "While drawing in class at Stagwood School, 12-year old Cal sees a frog staring at him through the window. Stranger than that, is the fact that this frog happens to be wearing glasses.",
                            storyTitle = "THE GUARDIANS OF LORE",
                            tagId = new Guid("acecd581-de79-498d-b4d6-c078a688e407")
                        },
                        new
                        {
                            storyId = new Guid("63874287-d204-4ad0-b9fe-f576885b605b"),
                            Position = 0,
                            genreId = new Guid("32a29cb8-b605-456d-82f2-76fb5564deee"),
                            publishDate = new DateTime(2024, 7, 12, 17, 52, 24, 842, DateTimeKind.Local).AddTicks(4677),
                            statusId = new Guid("2f85b983-e349-4a46-b829-a77cd30be50f"),
                            storyDescription = "Do you know what the Hippos of Africa do long after the Elephants and Rhinos have gone to sleep? This rhythmical story will teach kids about the habits of Hippos at night. Full of true facts and fun rhymes, kids stories don’t get more fun than this! ",
                            storyTitle = "WHEN DO HIPPOS PLAY?",
                            tagId = new Guid("48fa837c-6f38-4bf3-a52e-150d326f65d7")
                        });
                });

            modelBuilder.Entity("RaW.MVC.Data.Entities.Tag", b =>
                {
                    b.Property<Guid>("tagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<string>("tagDescription")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("tagName")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.HasKey("tagId");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            tagId = new Guid("2ce6c267-d3fc-4820-8f04-6547696bab28"),
                            Position = 0,
                            tagDescription = "Romance between male characters.",
                            tagName = "Boylove"
                        },
                        new
                        {
                            tagId = new Guid("e7196f16-0bf0-478f-8892-2ef83d09d14c"),
                            Position = 0,
                            tagDescription = "Romance between female characters.",
                            tagName = "Girllove"
                        },
                        new
                        {
                            tagId = new Guid("acecd581-de79-498d-b4d6-c078a688e407"),
                            Position = 0,
                            tagDescription = "Character with albinism.",
                            tagName = "Albino"
                        },
                        new
                        {
                            tagId = new Guid("48fa837c-6f38-4bf3-a52e-150d326f65d7"),
                            Position = 0,
                            tagDescription = "Character with pale skin tone.",
                            tagName = "Pale"
                        },
                        new
                        {
                            tagId = new Guid("01e61df7-c444-4555-a4ad-6b6cd8cb182f"),
                            Position = 0,
                            tagDescription = "Character with dark brown skin.",
                            tagName = "Dark brown skin tone"
                        });
                });

            modelBuilder.Entity("RaW.MVC.Data.Entities.Story", b =>
                {
                    b.HasOne("RaW.MVC.Data.Entities.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("genreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RaW.MVC.Data.Entities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("statusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RaW.MVC.Data.Entities.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("tagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Status");

                    b.Navigation("Tag");
                });
#pragma warning restore 612, 618
        }
    }
}
