using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaWMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_ccolumn_username_to_table_report : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Reports");
        }
    }
}
