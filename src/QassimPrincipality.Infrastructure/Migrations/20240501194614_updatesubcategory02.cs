using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QassimPrincipality.Infrastructure.Migrations
{
    public partial class updatesubcategory02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Audience",
                schema: "lookup",
                table: "EServiceSubCategory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DurationDays",
                schema: "lookup",
                table: "EServiceSubCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ServiceFees",
                schema: "lookup",
                table: "EServiceSubCategory",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ServiceRequierment",
                schema: "lookup",
                table: "EServiceSubCategory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Audience",
                schema: "lookup",
                table: "EServiceCategory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DurationDays",
                schema: "lookup",
                table: "EServiceCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ServiceFees",
                schema: "lookup",
                table: "EServiceCategory",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ServiceRequierment",
                schema: "lookup",
                table: "EServiceCategory",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Audience",
                schema: "lookup",
                table: "EServiceSubCategory");

            migrationBuilder.DropColumn(
                name: "DurationDays",
                schema: "lookup",
                table: "EServiceSubCategory");

            migrationBuilder.DropColumn(
                name: "ServiceFees",
                schema: "lookup",
                table: "EServiceSubCategory");

            migrationBuilder.DropColumn(
                name: "ServiceRequierment",
                schema: "lookup",
                table: "EServiceSubCategory");

            migrationBuilder.DropColumn(
                name: "Audience",
                schema: "lookup",
                table: "EServiceCategory");

            migrationBuilder.DropColumn(
                name: "DurationDays",
                schema: "lookup",
                table: "EServiceCategory");

            migrationBuilder.DropColumn(
                name: "ServiceFees",
                schema: "lookup",
                table: "EServiceCategory");

            migrationBuilder.DropColumn(
                name: "ServiceRequierment",
                schema: "lookup",
                table: "EServiceCategory");
        }
    }
}
