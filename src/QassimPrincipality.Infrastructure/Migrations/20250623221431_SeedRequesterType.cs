using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QassimPrincipality.Infrastructure.Migrations
{
    public partial class SeedRequesterType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RequesterType",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsActive", "NameAr", "NameEn", "UpdatedBy", "UpdatedOn" },
                values: new object[] { 1, "Admin", new DateTime(2025, 6, 24, 1, 14, 30, 896, DateTimeKind.Local).AddTicks(1124), false, "فرد", "Individual", null, null });

            migrationBuilder.InsertData(
                table: "RequesterType",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsActive", "NameAr", "NameEn", "UpdatedBy", "UpdatedOn" },
                values: new object[] { 2, "Admin", new DateTime(2025, 6, 24, 1, 14, 30, 896, DateTimeKind.Local).AddTicks(1134), false, "حكومة", "Government", null, null });

            migrationBuilder.InsertData(
                table: "RequesterType",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsActive", "NameAr", "NameEn", "UpdatedBy", "UpdatedOn" },
                values: new object[] { 3, "Admin", new DateTime(2025, 6, 24, 1, 14, 30, 896, DateTimeKind.Local).AddTicks(1135), false, "خاص", "Special", null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RequesterType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RequesterType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RequesterType",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
