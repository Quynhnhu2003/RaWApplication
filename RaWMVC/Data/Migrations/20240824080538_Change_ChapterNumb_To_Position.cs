﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaWMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class Change_ChapterNumb_To_Position : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "chapterNumb",
                table: "Chapters",
                newName: "Position");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Position",
                table: "Chapters",
                newName: "chapterNumb");
        }
    }
}
