using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QassimPrincipality.Infrastructure.Migrations
{
    public partial class RejectReasonShareData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RejectReason",
                schema: "services",
                table: "ShareDataRequest",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectReason",
                schema: "services",
                table: "ShareDataRequest");
        }
    }
}
