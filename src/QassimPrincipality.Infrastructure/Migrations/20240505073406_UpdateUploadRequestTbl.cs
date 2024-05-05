using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QassimPrincipality.Infrastructure.Migrations
{
    public partial class UpdateUploadRequestTbl : Migration
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
