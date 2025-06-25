using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QassimPrincipality.Infrastructure.Migrations
{
    public partial class AddShareDataRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntityType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShareDataRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferralNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentityNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityTypeId = table.Column<int>(type: "int", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RejectReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurposeOfRequest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsShareAgreementExist = table.Column<bool>(type: "bit", nullable: true),
                    IsContainsPersonalData = table.Column<bool>(type: "bit", nullable: true),
                    IsRequesterDataOfficePresenter = table.Column<bool>(type: "bit", nullable: true),
                    IsLegalJustification = table.Column<bool>(type: "bit", nullable: true),
                    IsAllowed = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: true),
                    LegalJustificationDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareDataRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShareDataRequests_EntityType_EntityTypeId",
                        column: x => x.EntityTypeId,
                        principalTable: "EntityType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ShareDataRequests_EntityTypeId",
                table: "ShareDataRequests",
                column: "EntityTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShareDataRequests");

            migrationBuilder.DropTable(
                name: "EntityType");

            migrationBuilder.UpdateData(
                table: "RequesterType",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 24, 1, 14, 30, 896, DateTimeKind.Local).AddTicks(1124));

            migrationBuilder.UpdateData(
                table: "RequesterType",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 24, 1, 14, 30, 896, DateTimeKind.Local).AddTicks(1134));

            migrationBuilder.UpdateData(
                table: "RequesterType",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2025, 6, 24, 1, 14, 30, 896, DateTimeKind.Local).AddTicks(1135));
        }
    }
}
