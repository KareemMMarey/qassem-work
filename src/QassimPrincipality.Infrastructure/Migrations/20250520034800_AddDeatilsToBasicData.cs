using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QassimPrincipality.Infrastructure.Migrations
{
    public partial class AddDeatilsToBasicData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestDetails",
                schema: "services",
                table: "RequestBasicData",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestDetails",
                schema: "services",
                table: "RequestBasicData");
        }
    }
}
