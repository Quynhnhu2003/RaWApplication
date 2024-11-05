using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RaWMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Property_In_All_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_Stories_storyId",
                table: "Chapters");

            migrationBuilder.DropForeignKey(
                name: "FK_Stories_Genres_genreId",
                table: "Stories");

            migrationBuilder.DropForeignKey(
                name: "FK_Stories_Status_statusId",
                table: "Stories");

            migrationBuilder.DropForeignKey(
                name: "FK_Stories_Tags_tagId",
                table: "Stories");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "genreId",
                keyValue: new Guid("06691ea9-d2cf-473b-a592-dbab69fe3ffe"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "genreId",
                keyValue: new Guid("32a29cb8-b605-456d-82f2-76fb5564deee"));

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "statusId",
                keyValue: new Guid("2f85b983-e349-4a46-b829-a77cd30be50f"));

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "statusId",
                keyValue: new Guid("bf3a1f0a-cb15-43f4-a270-30bf97762513"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "tagId",
                keyValue: new Guid("01e61df7-c444-4555-a4ad-6b6cd8cb182f"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "tagId",
                keyValue: new Guid("2ce6c267-d3fc-4820-8f04-6547696bab28"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "tagId",
                keyValue: new Guid("48fa837c-6f38-4bf3-a52e-150d326f65d7"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "tagId",
                keyValue: new Guid("acecd581-de79-498d-b4d6-c078a688e407"));

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "tagId",
                keyValue: new Guid("e7196f16-0bf0-478f-8892-2ef83d09d14c"));

            migrationBuilder.DropColumn(
                name: "IsFree",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "IsMature",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "PartCount",
                table: "Stories");

            migrationBuilder.RenameColumn(
                name: "tagName",
                table: "Tags",
                newName: "TagName");

            migrationBuilder.RenameColumn(
                name: "tagDescription",
                table: "Tags",
                newName: "TagDescription");

            migrationBuilder.RenameColumn(
                name: "tagId",
                table: "Stories",
                newName: "TagId");

            migrationBuilder.RenameColumn(
                name: "storyTitle",
                table: "Stories",
                newName: "StoryTitle");

            migrationBuilder.RenameColumn(
                name: "storyDescription",
                table: "Stories",
                newName: "StoryDescription");

            migrationBuilder.RenameColumn(
                name: "statusId",
                table: "Stories",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "publishDate",
                table: "Stories",
                newName: "PublishDate");

            migrationBuilder.RenameColumn(
                name: "genreId",
                table: "Stories",
                newName: "GenreId");

            migrationBuilder.RenameColumn(
                name: "coverImage",
                table: "Stories",
                newName: "CoverImage");

            migrationBuilder.RenameColumn(
                name: "storyId",
                table: "Stories",
                newName: "StoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Stories_tagId",
                table: "Stories",
                newName: "IX_Stories_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_Stories_statusId",
                table: "Stories",
                newName: "IX_Stories_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Stories_genreId",
                table: "Stories",
                newName: "IX_Stories_GenreId");

            migrationBuilder.RenameColumn(
                name: "statusName",
                table: "Status",
                newName: "StatusName");

            migrationBuilder.RenameColumn(
                name: "statusDescription",
                table: "Status",
                newName: "StatusDescription");

            migrationBuilder.RenameColumn(
                name: "genreName",
                table: "Genres",
                newName: "GenreName");

            migrationBuilder.RenameColumn(
                name: "genreDescription",
                table: "Genres",
                newName: "GenreDescription");

            migrationBuilder.RenameColumn(
                name: "storyId",
                table: "Chapters",
                newName: "StoryId");

            migrationBuilder.RenameColumn(
                name: "publishDate",
                table: "Chapters",
                newName: "PublishDate");

            migrationBuilder.RenameColumn(
                name: "chapterTitle",
                table: "Chapters",
                newName: "ChapterTitle");

            migrationBuilder.RenameColumn(
                name: "chapterContent",
                table: "Chapters",
                newName: "ChapterContent");

            migrationBuilder.RenameColumn(
                name: "chapterId",
                table: "Chapters",
                newName: "ChapterId");

            migrationBuilder.RenameIndex(
                name: "IX_Chapters_storyId",
                table: "Chapters",
                newName: "IX_Chapters_StoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_Stories_StoryId",
                table: "Chapters",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "StoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_Genres_GenreId",
                table: "Stories",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "genreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_Status_StatusId",
                table: "Stories",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "statusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_Tags_TagId",
                table: "Stories",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "tagId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_Stories_StoryId",
                table: "Chapters");

            migrationBuilder.DropForeignKey(
                name: "FK_Stories_Genres_GenreId",
                table: "Stories");

            migrationBuilder.DropForeignKey(
                name: "FK_Stories_Status_StatusId",
                table: "Stories");

            migrationBuilder.DropForeignKey(
                name: "FK_Stories_Tags_TagId",
                table: "Stories");

            migrationBuilder.RenameColumn(
                name: "TagName",
                table: "Tags",
                newName: "tagName");

            migrationBuilder.RenameColumn(
                name: "TagDescription",
                table: "Tags",
                newName: "tagDescription");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "Stories",
                newName: "tagId");

            migrationBuilder.RenameColumn(
                name: "StoryTitle",
                table: "Stories",
                newName: "storyTitle");

            migrationBuilder.RenameColumn(
                name: "StoryDescription",
                table: "Stories",
                newName: "storyDescription");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Stories",
                newName: "statusId");

            migrationBuilder.RenameColumn(
                name: "PublishDate",
                table: "Stories",
                newName: "publishDate");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "Stories",
                newName: "genreId");

            migrationBuilder.RenameColumn(
                name: "CoverImage",
                table: "Stories",
                newName: "coverImage");

            migrationBuilder.RenameColumn(
                name: "StoryId",
                table: "Stories",
                newName: "storyId");

            migrationBuilder.RenameIndex(
                name: "IX_Stories_TagId",
                table: "Stories",
                newName: "IX_Stories_tagId");

            migrationBuilder.RenameIndex(
                name: "IX_Stories_StatusId",
                table: "Stories",
                newName: "IX_Stories_statusId");

            migrationBuilder.RenameIndex(
                name: "IX_Stories_GenreId",
                table: "Stories",
                newName: "IX_Stories_genreId");

            migrationBuilder.RenameColumn(
                name: "StatusName",
                table: "Status",
                newName: "statusName");

            migrationBuilder.RenameColumn(
                name: "StatusDescription",
                table: "Status",
                newName: "statusDescription");

            migrationBuilder.RenameColumn(
                name: "GenreName",
                table: "Genres",
                newName: "genreName");

            migrationBuilder.RenameColumn(
                name: "GenreDescription",
                table: "Genres",
                newName: "genreDescription");

            migrationBuilder.RenameColumn(
                name: "StoryId",
                table: "Chapters",
                newName: "storyId");

            migrationBuilder.RenameColumn(
                name: "PublishDate",
                table: "Chapters",
                newName: "publishDate");

            migrationBuilder.RenameColumn(
                name: "ChapterTitle",
                table: "Chapters",
                newName: "chapterTitle");

            migrationBuilder.RenameColumn(
                name: "ChapterContent",
                table: "Chapters",
                newName: "chapterContent");

            migrationBuilder.RenameColumn(
                name: "ChapterId",
                table: "Chapters",
                newName: "chapterId");

            migrationBuilder.RenameIndex(
                name: "IX_Chapters_StoryId",
                table: "Chapters",
                newName: "IX_Chapters_storyId");

            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                table: "Stories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMature",
                table: "Stories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Stories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PartCount",
                table: "Stories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "genreId", "Position", "genreDescription", "genreName" },
                values: new object[,]
                {
                    { new Guid("06691ea9-d2cf-473b-a592-dbab69fe3ffe"), 0, "Fast-paced plot with intense events.", "Action" },
                    { new Guid("32a29cb8-b605-456d-82f2-76fb5564deee"), 0, "Exciting journey or quest.", "Adventure" }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "statusId", "Position", "statusDescription", "statusName" },
                values: new object[,]
                {
                    { new Guid("2f85b983-e349-4a46-b829-a77cd30be50f"), 0, "Fast-paced plot with intense events.", "Ongoing" },
                    { new Guid("bf3a1f0a-cb15-43f4-a270-30bf97762513"), 0, "Exciting journey or quest.", "Completed" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "tagId", "Position", "tagDescription", "tagName" },
                values: new object[,]
                {
                    { new Guid("01e61df7-c444-4555-a4ad-6b6cd8cb182f"), 0, "Character with dark brown skin.", "Dark brown skin tone" },
                    { new Guid("2ce6c267-d3fc-4820-8f04-6547696bab28"), 0, "Romance between male characters.", "Boylove" },
                    { new Guid("48fa837c-6f38-4bf3-a52e-150d326f65d7"), 0, "Character with pale skin tone.", "Pale" },
                    { new Guid("acecd581-de79-498d-b4d6-c078a688e407"), 0, "Character with albinism.", "Albino" },
                    { new Guid("e7196f16-0bf0-478f-8892-2ef83d09d14c"), 0, "Romance between female characters.", "Girllove" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_Stories_storyId",
                table: "Chapters",
                column: "storyId",
                principalTable: "Stories",
                principalColumn: "storyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_Genres_genreId",
                table: "Stories",
                column: "genreId",
                principalTable: "Genres",
                principalColumn: "genreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_Status_statusId",
                table: "Stories",
                column: "statusId",
                principalTable: "Status",
                principalColumn: "statusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_Tags_tagId",
                table: "Stories",
                column: "tagId",
                principalTable: "Tags",
                principalColumn: "tagId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
