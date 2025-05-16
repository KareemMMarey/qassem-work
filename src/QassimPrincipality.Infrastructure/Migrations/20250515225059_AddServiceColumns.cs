using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QassimPrincipality.Infrastructure.Migrations
{
    public partial class AddServiceColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasApplicantStatus",
                schema: "lookup",
                table: "EService",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasTypeOfSummons",
                schema: "lookup",
                table: "EService",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasApplicantStatus",
                schema: "lookup",
                table: "EService");

            migrationBuilder.DropColumn(
                name: "HasTypeOfSummons",
                schema: "lookup",
                table: "EService");
        }
    }
}
