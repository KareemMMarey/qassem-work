using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QassimPrincipality.Infrastructure.Migrations
{
    public partial class AddNewEServiceColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServiceActionMethos",
                schema: "lookup",
                table: "EService",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceController",
                schema: "lookup",
                table: "EService",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceActionMethos",
                schema: "lookup",
                table: "EService");

            migrationBuilder.DropColumn(
                name: "ServiceController",
                schema: "lookup",
                table: "EService");
        }
    }
}
