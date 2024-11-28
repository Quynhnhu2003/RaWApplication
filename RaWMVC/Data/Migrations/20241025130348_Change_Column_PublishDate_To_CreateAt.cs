﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaWMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class Change_Column_PublishDate_To_CreateAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublishDate",
                table: "Comments",
                newName: "CreateAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Comments",
                newName: "PublishDate");
        }
    }
}