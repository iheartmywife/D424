using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace helpmepickmymain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoleToClassRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Races_Factions_FactionID",
                table: "Races");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_WowClasses_WowClassId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_WowClassId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "WowClassId",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "FactionID",
                table: "Races",
                newName: "FactionId");

            migrationBuilder.RenameIndex(
                name: "IX_Races_FactionID",
                table: "Races",
                newName: "IX_Races_FactionId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Factions",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "RoleWowClass",
                columns: table => new
                {
                    RolesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WowClassesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleWowClass", x => new { x.RolesId, x.WowClassesId });
                    table.ForeignKey(
                        name: "FK_RoleWowClass_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleWowClass_WowClasses_WowClassesId",
                        column: x => x.WowClassesId,
                        principalTable: "WowClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleWowClass_WowClassesId",
                table: "RoleWowClass",
                column: "WowClassesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Factions_FactionId",
                table: "Races",
                column: "FactionId",
                principalTable: "Factions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Races_Factions_FactionId",
                table: "Races");

            migrationBuilder.DropTable(
                name: "RoleWowClass");

            migrationBuilder.RenameColumn(
                name: "FactionId",
                table: "Races",
                newName: "FactionID");

            migrationBuilder.RenameIndex(
                name: "IX_Races_FactionId",
                table: "Races",
                newName: "IX_Races_FactionID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Factions",
                newName: "ID");

            migrationBuilder.AddColumn<Guid>(
                name: "WowClassId",
                table: "Roles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_WowClassId",
                table: "Roles",
                column: "WowClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Races_Factions_FactionID",
                table: "Races",
                column: "FactionID",
                principalTable: "Factions",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_WowClasses_WowClassId",
                table: "Roles",
                column: "WowClassId",
                principalTable: "WowClasses",
                principalColumn: "Id");
        }
    }
}
