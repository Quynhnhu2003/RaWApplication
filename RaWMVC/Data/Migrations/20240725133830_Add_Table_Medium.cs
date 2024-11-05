using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaWMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Table_Medium : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MediumId",
                table: "Stories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stories_MediumId",
                table: "Stories",
                column: "MediumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_Media_MediumId",
                table: "Stories",
                column: "MediumId",
                principalTable: "Media",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_Media_MediumId",
                table: "Stories");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropIndex(
                name: "IX_Stories_MediumId",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "MediumId",
                table: "Stories");
        }
    }
}
