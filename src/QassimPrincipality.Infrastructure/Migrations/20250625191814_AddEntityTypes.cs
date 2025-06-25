using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QassimPrincipality.Infrastructure.Migrations
{
    public partial class AddEntityTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EntityType",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsActive", "NameAr", "NameEn", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, "Admin", new DateTime(2025, 6, 25, 22, 13, 27, 230, DateTimeKind.Local).AddTicks(141), false, "فرد", "Individual", null, null },
                    { 2, "Admin", new DateTime(2025, 6, 25, 22, 13, 27, 230, DateTimeKind.Local).AddTicks(141), false, "حكومة", "Government", null, null },
                    { 3, "Admin", new DateTime(2025, 6, 25, 22, 13, 27, 230, DateTimeKind.Local).AddTicks(141), false, "خاص", "Special", null, null }
                });

            migrationBuilder.UpdateData(
                table: "RequesterType",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 25, 22, 13, 27, 230, DateTimeKind.Local).AddTicks(141));

            migrationBuilder.UpdateData(
                table: "RequesterType",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 25, 22, 13, 27, 230, DateTimeKind.Local).AddTicks(141));

            migrationBuilder.UpdateData(
                table: "RequesterType",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 25, 22, 13, 27, 230, DateTimeKind.Local).AddTicks(141));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EntityType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EntityType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EntityType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "RequesterType",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 25, 22, 6, 39, 571, DateTimeKind.Local).AddTicks(8810));

            migrationBuilder.UpdateData(
                table: "RequesterType",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 25, 22, 6, 39, 571, DateTimeKind.Local).AddTicks(8819));

            migrationBuilder.UpdateData(
                table: "RequesterType",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 25, 22, 6, 39, 571, DateTimeKind.Local).AddTicks(8820));
        }
    }
}
