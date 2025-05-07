using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QassimPrincipality.Infrastructure.Migrations
{
    public partial class ChangeAboutSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                schema: "lookup",
                table: "AboutPageSection");

            migrationBuilder.AddColumn<string>(
                name: "AboutSectionType",
                schema: "lookup",
                table: "AboutPageSection",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutSectionType",
                schema: "lookup",
                table: "AboutPageSection");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "lookup",
                table: "AboutPageSection",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
