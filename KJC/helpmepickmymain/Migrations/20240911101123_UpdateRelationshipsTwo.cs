using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace helpmepickmymain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationshipsTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RaceWowClass_Races_RaceId",
                table: "RaceWowClass");

            migrationBuilder.DropForeignKey(
                name: "FK_RaceWowClass_WowClasses_WowClassId",
                table: "RaceWowClass");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleWowClass_Roles_RoleId",
                table: "RoleWowClass");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleWowClass_WowClasses_WowClassId",
                table: "RoleWowClass");

            migrationBuilder.DropForeignKey(
                name: "FK_Specs_Roles_RoleId",
                table: "Specs");

            migrationBuilder.RenameColumn(
                name: "WowClassId",
                table: "RoleWowClass",
                newName: "WowClassesId");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "RoleWowClass",
                newName: "RolesId");

            migrationBuilder.RenameIndex(
                name: "IX_RoleWowClass_WowClassId",
                table: "RoleWowClass",
                newName: "IX_RoleWowClass_WowClassesId");

            migrationBuilder.RenameColumn(
                name: "WowClassId",
                table: "RaceWowClass",
                newName: "WowClassesId");

            migrationBuilder.RenameColumn(
                name: "RaceId",
                table: "RaceWowClass",
                newName: "RacesId");

            migrationBuilder.RenameIndex(
                name: "IX_RaceWowClass_WowClassId",
                table: "RaceWowClass",
                newName: "IX_RaceWowClass_WowClassesId");

            migrationBuilder.AddForeignKey(
                name: "FK_RaceWowClass_Races_RacesId",
                table: "RaceWowClass",
                column: "RacesId",
                principalTable: "Races",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RaceWowClass_WowClasses_WowClassesId",
                table: "RaceWowClass",
                column: "WowClassesId",
                principalTable: "WowClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleWowClass_Roles_RolesId",
                table: "RoleWowClass",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleWowClass_WowClasses_WowClassesId",
                table: "RoleWowClass",
                column: "WowClassesId",
                principalTable: "WowClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Specs_Roles_RoleId",
                table: "Specs",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RaceWowClass_Races_RacesId",
                table: "RaceWowClass");

            migrationBuilder.DropForeignKey(
                name: "FK_RaceWowClass_WowClasses_WowClassesId",
                table: "RaceWowClass");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleWowClass_Roles_RolesId",
                table: "RoleWowClass");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleWowClass_WowClasses_WowClassesId",
                table: "RoleWowClass");

            migrationBuilder.DropForeignKey(
                name: "FK_Specs_Roles_RoleId",
                table: "Specs");

            migrationBuilder.RenameColumn(
                name: "WowClassesId",
                table: "RoleWowClass",
                newName: "WowClassId");

            migrationBuilder.RenameColumn(
                name: "RolesId",
                table: "RoleWowClass",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RoleWowClass_WowClassesId",
                table: "RoleWowClass",
                newName: "IX_RoleWowClass_WowClassId");

            migrationBuilder.RenameColumn(
                name: "WowClassesId",
                table: "RaceWowClass",
                newName: "WowClassId");

            migrationBuilder.RenameColumn(
                name: "RacesId",
                table: "RaceWowClass",
                newName: "RaceId");

            migrationBuilder.RenameIndex(
                name: "IX_RaceWowClass_WowClassesId",
                table: "RaceWowClass",
                newName: "IX_RaceWowClass_WowClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_RaceWowClass_Races_RaceId",
                table: "RaceWowClass",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RaceWowClass_WowClasses_WowClassId",
                table: "RaceWowClass",
                column: "WowClassId",
                principalTable: "WowClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleWowClass_Roles_RoleId",
                table: "RoleWowClass",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleWowClass_WowClasses_WowClassId",
                table: "RoleWowClass",
                column: "WowClassId",
                principalTable: "WowClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Specs_Roles_RoleId",
                table: "Specs",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
