using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QassimPrincipality.Infrastructure.Migrations
{
    public partial class AddServiceRequirements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "lookup",
                table: "EService",
                newName: "ServiceCode");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAr",
                schema: "lookup",
                table: "EService",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                schema: "lookup",
                table: "EService",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EServiceRequirement",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    IsPaper = table.Column<bool>(type: "bit", nullable: true),
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
                    table.PrimaryKey("PK_EServiceRequirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EServiceRequirement_EService_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "lookup",
                        principalTable: "EService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EServiceRequirement_ServiceId",
                schema: "lookup",
                table: "EServiceRequirement",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EServiceRequirement",
                schema: "lookup");

            migrationBuilder.DropColumn(
                name: "DescriptionAr",
                schema: "lookup",
                table: "EService");

            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                schema: "lookup",
                table: "EService");

            migrationBuilder.RenameColumn(
                name: "ServiceCode",
                schema: "lookup",
                table: "EService",
                newName: "Description");
        }
    }
}
