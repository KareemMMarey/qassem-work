using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QassimPrincipality.Infrastructure.Migrations
{
    public partial class RequestNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestNumber",
                schema: "services",
                table: "ServiceRequest",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestNumber",
                schema: "services",
                table: "ServiceRequest");
        }
    }
}
