using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QassimPrincipality.Infrastructure.Migrations
{
    public partial class ReferralNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "referralNumber",
                schema: "services",
                table: "UploadRequest",
                newName: "ReferralNumber");

            migrationBuilder.AddColumn<string>(
                name: "ReferralNumber",
                schema: "services",
                table: "ShareDataRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferralNumber",
                schema: "services",
                table: "OpenDataRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferralNumber",
                schema: "services",
                table: "ContactForm",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferralNumber",
                schema: "services",
                table: "ShareDataRequest");

            migrationBuilder.DropColumn(
                name: "ReferralNumber",
                schema: "services",
                table: "OpenDataRequest");

            migrationBuilder.DropColumn(
                name: "ReferralNumber",
                schema: "services",
                table: "ContactForm");

            migrationBuilder.RenameColumn(
                name: "ReferralNumber",
                schema: "services",
                table: "UploadRequest",
                newName: "referralNumber");
        }
    }
}
