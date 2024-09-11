using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace helpmepickmymain.Migrations
{
    /// <inheritdoc />
    public partial class Updatedb2Removedunnecessarydomainproperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Races_Factions_FactionId",
                table: "Races");

            migrationBuilder.DropForeignKey(
                name: "FK_Specs_WowClasses_WowClassId",
                table: "Specs");

            migrationBuilder.AlterColumn<Guid>(
                name: "WowClassId",
                table: "Specs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "FactionId",
                table: "Races",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Factions_FactionId",
                table: "Races",
                column: "FactionId",
                principalTable: "Factions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Specs_WowClasses_WowClassId",
                table: "Specs",
                column: "WowClassId",
                principalTable: "WowClasses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Races_Factions_FactionId",
                table: "Races");

            migrationBuilder.DropForeignKey(
                name: "FK_Specs_WowClasses_WowClassId",
                table: "Specs");

            migrationBuilder.AlterColumn<Guid>(
                name: "WowClassId",
                table: "Specs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FactionId",
                table: "Races",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Factions_FactionId",
                table: "Races",
                column: "FactionId",
                principalTable: "Factions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Specs_WowClasses_WowClassId",
                table: "Specs",
                column: "WowClassId",
                principalTable: "WowClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
