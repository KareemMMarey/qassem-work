using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QassimPrincipality.Infrastructure.Migrations
{
    public partial class AddFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NafathNumber",
                schema: "services",
                table: "UploadRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "services",
                table: "UploadRequest",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NafathNumber",
                schema: "services",
                table: "UploadRequest");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "services",
                table: "UploadRequest");
        }
    }
}
