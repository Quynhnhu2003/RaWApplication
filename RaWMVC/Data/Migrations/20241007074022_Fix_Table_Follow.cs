using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaWMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Table_Follow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Follows_RaWMVCUser_FollowedId",
                table: "Follows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Follows",
                table: "Follows");

            migrationBuilder.RenameColumn(
                name: "FollowedId",
                table: "Follows",
                newName: "FollowingId");

            migrationBuilder.RenameIndex(
                name: "IX_Follows_FollowedId",
                table: "Follows",
                newName: "IX_Follows_FollowingId");

            migrationBuilder.AddColumn<int>(
                name: "ReadingListsId",
                table: "Stories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "RaWMVCUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "JoinedDate",
                table: "RaWMVCUser",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Follows",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Follows",
                table: "Follows",
                columns: new[] { "FollowerId", "Id" });

            migrationBuilder.CreateTable(
                name: "ReadingLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReadingLists_RaWMVCUser_UserId",
                        column: x => x.UserId,
                        principalTable: "RaWMVCUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stories_ReadingListsId",
                table: "Stories",
                column: "ReadingListsId");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingLists_UserId",
                table: "ReadingLists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_RaWMVCUser_FollowingId",
                table: "Follows",
                column: "FollowingId",
                principalTable: "RaWMVCUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_ReadingLists_ReadingListsId",
                table: "Stories",
                column: "ReadingListsId",
                principalTable: "ReadingLists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Follows_RaWMVCUser_FollowingId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Stories_ReadingLists_ReadingListsId",
                table: "Stories");

            migrationBuilder.DropTable(
                name: "ReadingLists");

            migrationBuilder.DropIndex(
                name: "IX_Stories_ReadingListsId",
                table: "Stories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Follows",
                table: "Follows");

            migrationBuilder.DropColumn(
                name: "ReadingListsId",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "RaWMVCUser");

            migrationBuilder.DropColumn(
                name: "JoinedDate",
                table: "RaWMVCUser");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Follows");

            migrationBuilder.RenameColumn(
                name: "FollowingId",
                table: "Follows",
                newName: "FollowedId");

            migrationBuilder.RenameIndex(
                name: "IX_Follows_FollowingId",
                table: "Follows",
                newName: "IX_Follows_FollowedId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Follows",
                table: "Follows",
                columns: new[] { "FollowerId", "FollowedId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_RaWMVCUser_FollowedId",
                table: "Follows",
                column: "FollowedId",
                principalTable: "RaWMVCUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
