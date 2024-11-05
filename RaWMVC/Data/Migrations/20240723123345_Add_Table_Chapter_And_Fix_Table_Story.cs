using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RaWMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Table_Chapter_And_Fix_Table_Story : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stories",
                keyColumn: "storyId",
                keyValue: new Guid("15d1df89-fe11-45e5-88c9-2a2b4ec0d171"));

            migrationBuilder.DeleteData(
                table: "Stories",
                keyColumn: "storyId",
                keyValue: new Guid("63874287-d204-4ad0-b9fe-f576885b605b"));

            migrationBuilder.AlterColumn<string>(
                name: "statusDescription",
                table: "Status",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "genreDescription",
                table: "Genres",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(75)",
                oldMaxLength: 75);

            migrationBuilder.CreateTable(
                name: "Chapters",
                columns: table => new
                {
                    chapterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    chapterTitle = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    chapterNumb = table.Column<int>(type: "int", nullable: false),
                    chapterContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    publishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    storyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapters", x => x.chapterId);
                    table.ForeignKey(
                        name: "FK_Chapters_Stories_storyId",
                        column: x => x.storyId,
                        principalTable: "Stories",
                        principalColumn: "storyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Stories",
                columns: new[] { "storyId", "Position", "coverImage", "genreId", "publishDate", "statusId", "storyDescription", "storyTitle", "tagId" },
                values: new object[,]
                {
                    { new Guid("4c0715f9-8f94-42cc-ae60-9dd7d39f0f3a"), 0, null, new Guid("32a29cb8-b605-456d-82f2-76fb5564deee"), new DateTime(2024, 7, 23, 19, 33, 44, 785, DateTimeKind.Local).AddTicks(8114), new Guid("2f85b983-e349-4a46-b829-a77cd30be50f"), "While drawing in class at Stagwood School, 12-year old Cal sees a frog staring at him through the window. Stranger than that, is the fact that this frog happens to be wearing glasses.", "THE GUARDIANS OF LORE", new Guid("acecd581-de79-498d-b4d6-c078a688e407") },
                    { new Guid("bd6164fb-1fe9-4b3b-bfbb-80decdc28640"), 0, null, new Guid("32a29cb8-b605-456d-82f2-76fb5564deee"), new DateTime(2024, 7, 23, 19, 33, 44, 785, DateTimeKind.Local).AddTicks(8133), new Guid("2f85b983-e349-4a46-b829-a77cd30be50f"), "Do you know what the Hippos of Africa do long after the Elephants and Rhinos have gone to sleep? This rhythmical story will teach kids about the habits of Hippos at night. Full of true facts and fun rhymes, kids stories don’t get more fun than this! ", "WHEN DO HIPPOS PLAY?", new Guid("48fa837c-6f38-4bf3-a52e-150d326f65d7") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_storyId",
                table: "Chapters",
                column: "storyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chapters");

            migrationBuilder.DeleteData(
                table: "Stories",
                keyColumn: "storyId",
                keyValue: new Guid("4c0715f9-8f94-42cc-ae60-9dd7d39f0f3a"));

            migrationBuilder.DeleteData(
                table: "Stories",
                keyColumn: "storyId",
                keyValue: new Guid("bd6164fb-1fe9-4b3b-bfbb-80decdc28640"));

            migrationBuilder.AlterColumn<string>(
                name: "statusDescription",
                table: "Status",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "genreDescription",
                table: "Genres",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(75)",
                oldMaxLength: 75,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Stories",
                columns: new[] { "storyId", "Position", "coverImage", "genreId", "publishDate", "statusId", "storyDescription", "storyTitle", "tagId" },
                values: new object[,]
                {
                    { new Guid("15d1df89-fe11-45e5-88c9-2a2b4ec0d171"), 0, null, new Guid("32a29cb8-b605-456d-82f2-76fb5564deee"), new DateTime(2024, 7, 12, 17, 52, 24, 842, DateTimeKind.Local).AddTicks(4657), new Guid("2f85b983-e349-4a46-b829-a77cd30be50f"), "While drawing in class at Stagwood School, 12-year old Cal sees a frog staring at him through the window. Stranger than that, is the fact that this frog happens to be wearing glasses.", "THE GUARDIANS OF LORE", new Guid("acecd581-de79-498d-b4d6-c078a688e407") },
                    { new Guid("63874287-d204-4ad0-b9fe-f576885b605b"), 0, null, new Guid("32a29cb8-b605-456d-82f2-76fb5564deee"), new DateTime(2024, 7, 12, 17, 52, 24, 842, DateTimeKind.Local).AddTicks(4677), new Guid("2f85b983-e349-4a46-b829-a77cd30be50f"), "Do you know what the Hippos of Africa do long after the Elephants and Rhinos have gone to sleep? This rhythmical story will teach kids about the habits of Hippos at night. Full of true facts and fun rhymes, kids stories don’t get more fun than this! ", "WHEN DO HIPPOS PLAY?", new Guid("48fa837c-6f38-4bf3-a52e-150d326f65d7") }
                });
        }
    }
}
