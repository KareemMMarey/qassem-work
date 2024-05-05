using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QassimPrincipality.Infrastructure.Migrations
{
    public partial class UpdateForms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsApproved",
                schema: "services",
                table: "UploadRequest",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByFullName",
                schema: "services",
                table: "UploadRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectReason",
                schema: "services",
                table: "UploadRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                schema: "services",
                table: "ShareDataRequest",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "IdentityNumber",
                schema: "services",
                table: "OpenDataRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                schema: "services",
                table: "OpenDataRequest",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "services",
                table: "OpenDataRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityNumber",
                schema: "services",
                table: "ContactForm",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByFullName",
                schema: "services",
                table: "UploadRequest");

            migrationBuilder.DropColumn(
                name: "RejectReason",
                schema: "services",
                table: "UploadRequest");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                schema: "services",
                table: "ShareDataRequest");

            migrationBuilder.DropColumn(
                name: "IdentityNumber",
                schema: "services",
                table: "OpenDataRequest");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                schema: "services",
                table: "OpenDataRequest");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "services",
                table: "OpenDataRequest");

            migrationBuilder.DropColumn(
                name: "IdentityNumber",
                schema: "services",
                table: "ContactForm");

            migrationBuilder.AlterColumn<bool>(
                name: "IsApproved",
                schema: "services",
                table: "UploadRequest",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
