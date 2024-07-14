using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RaWMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Position_To_All_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stories",
                keyColumn: "storyId",
                keyValue: new Guid("34629ae0-3d15-40a9-8e6c-cb49a51829a0"));

            migrationBuilder.DeleteData(
                table: "Stories",
                keyColumn: "storyId",
                keyValue: new Guid("e5f4156c-76fa-4a38-a63c-352d72de9cca"));

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Stories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Status",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Genres",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "genreId",
                keyValue: new Guid("06691ea9-d2cf-473b-a592-dbab69fe3ffe"),
                column: "Position",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "genreId",
                keyValue: new Guid("32a29cb8-b605-456d-82f2-76fb5564deee"),
                column: "Position",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "statusId",
                keyValue: new Guid("2f85b983-e349-4a46-b829-a77cd30be50f"),
                column: "Position",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "statusId",
                keyValue: new Guid("bf3a1f0a-cb15-43f4-a270-30bf97762513"),
                column: "Position",
                value: 0);

            migrationBuilder.InsertData(
                table: "Stories",
                columns: new[] { "storyId", "Position", "coverImage", "genreId", "publishDate", "statusId", "storyDescription", "storyTitle", "tagId" },
                values: new object[,]
                {
                    { new Guid("15d1df89-fe11-45e5-88c9-2a2b4ec0d171"), 0, null, new Guid("32a29cb8-b605-456d-82f2-76fb5564deee"), new DateTime(2024, 7, 12, 17, 52, 24, 842, DateTimeKind.Local).AddTicks(4657), new Guid("2f85b983-e349-4a46-b829-a77cd30be50f"), "While drawing in class at Stagwood School, 12-year old Cal sees a frog staring at him through the window. Stranger than that, is the fact that this frog happens to be wearing glasses.", "THE GUARDIANS OF LORE", new Guid("acecd581-de79-498d-b4d6-c078a688e407") },
                    { new Guid("63874287-d204-4ad0-b9fe-f576885b605b"), 0, null, new Guid("32a29cb8-b605-456d-82f2-76fb5564deee"), new DateTime(2024, 7, 12, 17, 52, 24, 842, DateTimeKind.Local).AddTicks(4677), new Guid("2f85b983-e349-4a46-b829-a77cd30be50f"), "Do you know what the Hippos of Africa do long after the Elephants and Rhinos have gone to sleep? This rhythmical story will teach kids about the habits of Hippos at night. Full of true facts and fun rhymes, kids stories don’t get more fun than this! ", "WHEN DO HIPPOS PLAY?", new Guid("48fa837c-6f38-4bf3-a52e-150d326f65d7") }
                });

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "tagId",
                keyValue: new Guid("01e61df7-c444-4555-a4ad-6b6cd8cb182f"),
                column: "Position",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "tagId",
                keyValue: new Guid("2ce6c267-d3fc-4820-8f04-6547696bab28"),
                column: "Position",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "tagId",
                keyValue: new Guid("48fa837c-6f38-4bf3-a52e-150d326f65d7"),
                column: "Position",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "tagId",
                keyValue: new Guid("acecd581-de79-498d-b4d6-c078a688e407"),
                column: "Position",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Tags",
                keyColumn: "tagId",
                keyValue: new Guid("e7196f16-0bf0-478f-8892-2ef83d09d14c"),
                column: "Position",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stories",
                keyColumn: "storyId",
                keyValue: new Guid("15d1df89-fe11-45e5-88c9-2a2b4ec0d171"));

            migrationBuilder.DeleteData(
                table: "Stories",
                keyColumn: "storyId",
                keyValue: new Guid("63874287-d204-4ad0-b9fe-f576885b605b"));

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Genres");

            migrationBuilder.InsertData(
                table: "Stories",
                columns: new[] { "storyId", "coverImage", "genreId", "publishDate", "statusId", "storyDescription", "storyTitle", "tagId" },
                values: new object[,]
                {
                    { new Guid("34629ae0-3d15-40a9-8e6c-cb49a51829a0"), null, new Guid("32a29cb8-b605-456d-82f2-76fb5564deee"), new DateTime(2024, 7, 12, 16, 40, 19, 312, DateTimeKind.Local).AddTicks(9666), new Guid("2f85b983-e349-4a46-b829-a77cd30be50f"), "Do you know what the Hippos of Africa do long after the Elephants and Rhinos have gone to sleep? This rhythmical story will teach kids about the habits of Hippos at night. Full of true facts and fun rhymes, kids stories don’t get more fun than this! ", "WHEN DO HIPPOS PLAY?", new Guid("48fa837c-6f38-4bf3-a52e-150d326f65d7") },
                    { new Guid("e5f4156c-76fa-4a38-a63c-352d72de9cca"), null, new Guid("32a29cb8-b605-456d-82f2-76fb5564deee"), new DateTime(2024, 7, 12, 16, 40, 19, 312, DateTimeKind.Local).AddTicks(9647), new Guid("2f85b983-e349-4a46-b829-a77cd30be50f"), "While drawing in class at Stagwood School, 12-year old Cal sees a frog staring at him through the window. Stranger than that, is the fact that this frog happens to be wearing glasses.", "THE GUARDIANS OF LORE", new Guid("acecd581-de79-498d-b4d6-c078a688e407") }
                });
        }
    }
}
