using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaWMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fix_relationship_between_library_and_story : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libraries_ReadingList_ReadingListsId",
                table: "Libraries");

            migrationBuilder.DropForeignKey(
                name: "FK_Libraries_Stories_StoryId",
                table: "Libraries");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadingListStory_ReadingList_ReadingListId",
                table: "ReadingListStory");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadingListStory_Stories_StoryId",
                table: "ReadingListStory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReadingListStory",
                table: "ReadingListStory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReadingList",
                table: "ReadingList");

            migrationBuilder.RenameTable(
                name: "ReadingListStory",
                newName: "ReadingListStories");

            migrationBuilder.RenameTable(
                name: "ReadingList",
                newName: "ReadingLists");

            migrationBuilder.RenameIndex(
                name: "IX_ReadingListStory_StoryId",
                table: "ReadingListStories",
                newName: "IX_ReadingListStories_StoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReadingListStories",
                table: "ReadingListStories",
                columns: new[] { "ReadingListId", "StoryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReadingLists",
                table: "ReadingLists",
                column: "ReadingListsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Libraries_ReadingLists_ReadingListsId",
                table: "Libraries",
                column: "ReadingListsId",
                principalTable: "ReadingLists",
                principalColumn: "ReadingListsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Libraries_Stories_StoryId",
                table: "Libraries",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "StoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadingListStories_ReadingLists_ReadingListId",
                table: "ReadingListStories",
                column: "ReadingListId",
                principalTable: "ReadingLists",
                principalColumn: "ReadingListsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadingListStories_Stories_StoryId",
                table: "ReadingListStories",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "StoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libraries_ReadingLists_ReadingListsId",
                table: "Libraries");

            migrationBuilder.DropForeignKey(
                name: "FK_Libraries_Stories_StoryId",
                table: "Libraries");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadingListStories_ReadingLists_ReadingListId",
                table: "ReadingListStories");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadingListStories_Stories_StoryId",
                table: "ReadingListStories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReadingListStories",
                table: "ReadingListStories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReadingLists",
                table: "ReadingLists");

            migrationBuilder.RenameTable(
                name: "ReadingListStories",
                newName: "ReadingListStory");

            migrationBuilder.RenameTable(
                name: "ReadingLists",
                newName: "ReadingList");

            migrationBuilder.RenameIndex(
                name: "IX_ReadingListStories_StoryId",
                table: "ReadingListStory",
                newName: "IX_ReadingListStory_StoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReadingListStory",
                table: "ReadingListStory",
                columns: new[] { "ReadingListId", "StoryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReadingList",
                table: "ReadingList",
                column: "ReadingListsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Libraries_ReadingList_ReadingListsId",
                table: "Libraries",
                column: "ReadingListsId",
                principalTable: "ReadingList",
                principalColumn: "ReadingListsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Libraries_Stories_StoryId",
                table: "Libraries",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "StoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadingListStory_ReadingList_ReadingListId",
                table: "ReadingListStory",
                column: "ReadingListId",
                principalTable: "ReadingList",
                principalColumn: "ReadingListsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadingListStory_Stories_StoryId",
                table: "ReadingListStory",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "StoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
