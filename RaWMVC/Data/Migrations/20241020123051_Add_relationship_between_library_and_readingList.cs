using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaWMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_relationship_between_library_and_readingList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReadingListsId",
                table: "Libraries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Libraries_ReadingListsId",
                table: "Libraries",
                column: "ReadingListsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Libraries_ReadingList_ReadingListsId",
                table: "Libraries",
                column: "ReadingListsId",
                principalTable: "ReadingList",
                principalColumn: "ReadingListsId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libraries_ReadingList_ReadingListsId",
                table: "Libraries");

            migrationBuilder.DropIndex(
                name: "IX_Libraries_ReadingListsId",
                table: "Libraries");

            migrationBuilder.DropColumn(
                name: "ReadingListsId",
                table: "Libraries");
        }
    }
}
