﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace helpmepickmymain.Migrations
{
    /// <inheritdoc />
    public partial class AddDisplayNameToRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Roles");
        }
    }
}
