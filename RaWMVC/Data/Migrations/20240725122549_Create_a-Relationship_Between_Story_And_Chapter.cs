using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RaWMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create_aRelationship_Between_Story_And_Chapter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stories",
                keyColumn: "storyId",
                keyValue: new Guid("4c0715f9-8f94-42cc-ae60-9dd7d39f0f3a"));

            migrationBuilder.DeleteData(
                table: "Stories",
                keyColumn: "storyId",
                keyValue: new Guid("bd6164fb-1fe9-4b3b-bfbb-80decdc28640"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Stories",
                columns: new[] { "storyId", "Position", "coverImage", "genreId", "publishDate", "statusId", "storyDescription", "storyTitle", "tagId" },
                values: new object[,]
                {
                    { new Guid("4c0715f9-8f94-42cc-ae60-9dd7d39f0f3a"), 0, null, new Guid("32a29cb8-b605-456d-82f2-76fb5564deee"), new DateTime(2024, 7, 23, 19, 33, 44, 785, DateTimeKind.Local).AddTicks(8114), new Guid("2f85b983-e349-4a46-b829-a77cd30be50f"), "While drawing in class at Stagwood School, 12-year old Cal sees a frog staring at him through the window. Stranger than that, is the fact that this frog happens to be wearing glasses.", "THE GUARDIANS OF LORE", new Guid("acecd581-de79-498d-b4d6-c078a688e407") },
                    { new Guid("bd6164fb-1fe9-4b3b-bfbb-80decdc28640"), 0, null, new Guid("32a29cb8-b605-456d-82f2-76fb5564deee"), new DateTime(2024, 7, 23, 19, 33, 44, 785, DateTimeKind.Local).AddTicks(8133), new Guid("2f85b983-e349-4a46-b829-a77cd30be50f"), "Do you know what the Hippos of Africa do long after the Elephants and Rhinos have gone to sleep? This rhythmical story will teach kids about the habits of Hippos at night. Full of true facts and fun rhymes, kids stories don’t get more fun than this! ", "WHEN DO HIPPOS PLAY?", new Guid("48fa837c-6f38-4bf3-a52e-150d326f65d7") }
                });
        }
    }
}
