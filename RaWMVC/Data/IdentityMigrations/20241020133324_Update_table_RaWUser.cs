using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaWMVC.Data.IdentityMigrations
{
    /// <inheritdoc />
    public partial class Update_table_RaWUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrentListId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentListId",
                table: "AspNetUsers");
        }
    }
}
