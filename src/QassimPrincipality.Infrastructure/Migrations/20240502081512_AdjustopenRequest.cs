using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QassimPrincipality.Infrastructure.Migrations
{
    public partial class AdjustopenRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAllowed",
                schema: "services",
                table: "ShareDataRequest",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowed",
                schema: "services",
                table: "OpenDataRequest",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                schema: "services",
                table: "ContactForm",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAllowed",
                schema: "services",
                table: "ShareDataRequest");

            migrationBuilder.DropColumn(
                name: "IsAllowed",
                schema: "services",
                table: "OpenDataRequest");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                schema: "services",
                table: "ContactForm");
        }
    }
}
