using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaWMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Story_ID_In_Chapter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_Stories_storyId",
                table: "Chapters");

            migrationBuilder.AlterColumn<Guid>(
                name: "storyId",
                table: "Chapters",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_Stories_storyId",
                table: "Chapters",
                column: "storyId",
                principalTable: "Stories",
                principalColumn: "storyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_Stories_storyId",
                table: "Chapters");

            migrationBuilder.AlterColumn<Guid>(
                name: "storyId",
                table: "Chapters",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_Stories_storyId",
                table: "Chapters",
                column: "storyId",
                principalTable: "Stories",
                principalColumn: "storyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
