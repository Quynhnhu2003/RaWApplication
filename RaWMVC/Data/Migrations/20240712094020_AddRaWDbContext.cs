using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RaWMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRaWDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    genreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    genreName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    genreDescription = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.genreId);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    statusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    statusName = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    statusDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.statusId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    tagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tagName = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    tagDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.tagId);
                });

            migrationBuilder.CreateTable(
                name: "Stories",
                columns: table => new
                {
                    storyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    storyTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    storyDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    coverImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    publishDate = table.Column<DateTime>(type: "datetime2", maxLength: 10, nullable: false),
                    genreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    statusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stories", x => x.storyId);
                    table.ForeignKey(
                        name: "FK_Stories_Genres_genreId",
                        column: x => x.genreId,
                        principalTable: "Genres",
                        principalColumn: "genreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stories_Status_statusId",
                        column: x => x.statusId,
                        principalTable: "Status",
                        principalColumn: "statusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stories_Tags_tagId",
                        column: x => x.tagId,
                        principalTable: "Tags",
                        principalColumn: "tagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "genreId", "genreDescription", "genreName" },
                values: new object[,]
                {
                    { new Guid("06691ea9-d2cf-473b-a592-dbab69fe3ffe"), "Fast-paced plot with intense events.", "Action" },
                    { new Guid("32a29cb8-b605-456d-82f2-76fb5564deee"), "Exciting journey or quest.", "Adventure" }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "statusId", "statusDescription", "statusName" },
                values: new object[,]
                {
                    { new Guid("2f85b983-e349-4a46-b829-a77cd30be50f"), "Fast-paced plot with intense events.", "Ongoing" },
                    { new Guid("bf3a1f0a-cb15-43f4-a270-30bf97762513"), "Exciting journey or quest.", "Completed" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "tagId", "tagDescription", "tagName" },
                values: new object[,]
                {
                    { new Guid("01e61df7-c444-4555-a4ad-6b6cd8cb182f"), "Character with dark brown skin.", "Dark brown skin tone" },
                    { new Guid("2ce6c267-d3fc-4820-8f04-6547696bab28"), "Romance between male characters.", "Boylove" },
                    { new Guid("48fa837c-6f38-4bf3-a52e-150d326f65d7"), "Character with pale skin tone.", "Pale" },
                    { new Guid("acecd581-de79-498d-b4d6-c078a688e407"), "Character with albinism.", "Albino" },
                    { new Guid("e7196f16-0bf0-478f-8892-2ef83d09d14c"), "Romance between female characters.", "Girllove" }
                });

            migrationBuilder.InsertData(
                table: "Stories",
                columns: new[] { "storyId", "coverImage", "genreId", "publishDate", "statusId", "storyDescription", "storyTitle", "tagId" },
                values: new object[,]
                {
                    { new Guid("34629ae0-3d15-40a9-8e6c-cb49a51829a0"), null, new Guid("32a29cb8-b605-456d-82f2-76fb5564deee"), new DateTime(2024, 7, 12, 16, 40, 19, 312, DateTimeKind.Local).AddTicks(9666), new Guid("2f85b983-e349-4a46-b829-a77cd30be50f"), "Do you know what the Hippos of Africa do long after the Elephants and Rhinos have gone to sleep? This rhythmical story will teach kids about the habits of Hippos at night. Full of true facts and fun rhymes, kids stories don’t get more fun than this! ", "WHEN DO HIPPOS PLAY?", new Guid("48fa837c-6f38-4bf3-a52e-150d326f65d7") },
                    { new Guid("e5f4156c-76fa-4a38-a63c-352d72de9cca"), null, new Guid("32a29cb8-b605-456d-82f2-76fb5564deee"), new DateTime(2024, 7, 12, 16, 40, 19, 312, DateTimeKind.Local).AddTicks(9647), new Guid("2f85b983-e349-4a46-b829-a77cd30be50f"), "While drawing in class at Stagwood School, 12-year old Cal sees a frog staring at him through the window. Stranger than that, is the fact that this frog happens to be wearing glasses.", "THE GUARDIANS OF LORE", new Guid("acecd581-de79-498d-b4d6-c078a688e407") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stories_genreId",
                table: "Stories",
                column: "genreId");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_statusId",
                table: "Stories",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_tagId",
                table: "Stories",
                column: "tagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stories");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
