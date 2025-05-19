using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QassimPrincipality.Infrastructure.Migrations
{
    public partial class AddColumnToAttachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionAr",
                schema: "lookup",
                table: "AttachmentType",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                schema: "lookup",
                table: "AttachmentType",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionAr",
                schema: "lookup",
                table: "AttachmentType");

            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                schema: "lookup",
                table: "AttachmentType");
        }
    }
}
