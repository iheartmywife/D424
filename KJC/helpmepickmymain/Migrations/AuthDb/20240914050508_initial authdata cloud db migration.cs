using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace helpmepickmymain.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class initialauthdataclouddbmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0020b04d-9e61-498f-8f2e-33721079ae4d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d6decbb-38f0-47ec-abb4-4e81963f453e", "AQAAAAIAAYagAAAAEGVcV+ma2ksWMoXAWC/+4VF31AUGKdRykg4L40TLsnXJEq6YrWMkFslnQsH7mH9/yA==", "a77e1793-fdae-4219-96ff-5fa5c4117ed1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0020b04d-9e61-498f-8f2e-33721079ae4d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e42aac2b-cfc5-43ef-92d3-4ab6326088ed", "AQAAAAIAAYagAAAAEE9gE+WmD9fQFm+2Xh/lCubvdtR9b3QXy8dXLPQSBFhtjl614rd4RaHbyj6siV2fSA==", "8f28b8d1-f0e1-4276-a659-54f469831153" });
        }
    }
}
