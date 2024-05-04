using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QassimPrincipality.Infrastructure.Migrations
{
    public partial class updatesubcategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EServiceSubCategory_EServiceCategory_EServiceCategoryId",
                schema: "lookup",
                table: "EServiceSubCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_UploadRequest_EServiceSubCategory_EServiceSubCategoryId",
                schema: "services",
                table: "UploadRequest");

            migrationBuilder.DropIndex(
                name: "IX_UploadRequest_EServiceSubCategoryId",
                schema: "services",
                table: "UploadRequest");

            migrationBuilder.DropIndex(
                name: "IX_EServiceSubCategory_EServiceCategoryId",
                schema: "lookup",
                table: "EServiceSubCategory");

            migrationBuilder.DropColumn(
                name: "EServiceSubCategoryId",
                schema: "services",
                table: "UploadRequest");

            migrationBuilder.DropColumn(
                name: "ExecutiveSummary",
                schema: "services",
                table: "UploadRequest");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "services",
                table: "UploadRequest");

            migrationBuilder.DropColumn(
                name: "OriginalRequestId",
                schema: "services",
                table: "UploadRequest");

            migrationBuilder.DropColumn(
                name: "OriginatorId",
                schema: "services",
                table: "UploadRequest");

            migrationBuilder.DropColumn(
                name: "RequestNumber",
                schema: "services",
                table: "UploadRequest");

            migrationBuilder.DropColumn(
                name: "RequestOwnerId",
                schema: "services",
                table: "UploadRequest");

            migrationBuilder.DropColumn(
                name: "RequestOwnerNameAr",
                schema: "services",
                table: "UploadRequest");

            migrationBuilder.DropColumn(
                name: "RequestSubClassificationId",
                schema: "services",
                table: "UploadRequest");

            migrationBuilder.DropColumn(
                name: "EServiceCategoryId",
                schema: "lookup",
                table: "EServiceSubCategory");

            migrationBuilder.RenameColumn(
                name: "RequestSource",
                schema: "services",
                table: "UploadRequest",
                newName: "OracleRequestNumber");

            migrationBuilder.AlterColumn<bool>(
                name: "IsApproved",
                schema: "services",
                table: "UploadRequest",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                schema: "lookup",
                table: "EServiceSubCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasSubCategory",
                schema: "lookup",
                table: "EServiceCategory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_EServiceSubCategory_CategoryId",
                schema: "lookup",
                table: "EServiceSubCategory",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_EServiceSubCategory_EServiceCategory_CategoryId",
                schema: "lookup",
                table: "EServiceSubCategory",
                column: "CategoryId",
                principalSchema: "lookup",
                principalTable: "EServiceCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EServiceSubCategory_EServiceCategory_CategoryId",
                schema: "lookup",
                table: "EServiceSubCategory");

            migrationBuilder.DropIndex(
                name: "IX_EServiceSubCategory_CategoryId",
                schema: "lookup",
                table: "EServiceSubCategory");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                schema: "lookup",
                table: "EServiceSubCategory");

            migrationBuilder.DropColumn(
                name: "HasSubCategory",
                schema: "lookup",
                table: "EServiceCategory");

            migrationBuilder.RenameColumn(
                name: "OracleRequestNumber",
                schema: "services",
                table: "UploadRequest",
                newName: "RequestSource");

            migrationBuilder.AlterColumn<bool>(
                name: "IsApproved",
                schema: "services",
                table: "UploadRequest",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "EServiceSubCategoryId",
                schema: "services",
                table: "UploadRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ExecutiveSummary",
                schema: "services",
                table: "UploadRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "services",
                table: "UploadRequest",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OriginalRequestId",
                schema: "services",
                table: "UploadRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OriginatorId",
                schema: "services",
                table: "UploadRequest",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestNumber",
                schema: "services",
                table: "UploadRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RequestOwnerId",
                schema: "services",
                table: "UploadRequest",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestOwnerNameAr",
                schema: "services",
                table: "UploadRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestSubClassificationId",
                schema: "services",
                table: "UploadRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EServiceCategoryId",
                schema: "lookup",
                table: "EServiceSubCategory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UploadRequest_EServiceSubCategoryId",
                schema: "services",
                table: "UploadRequest",
                column: "EServiceSubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EServiceSubCategory_EServiceCategoryId",
                schema: "lookup",
                table: "EServiceSubCategory",
                column: "EServiceCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_EServiceSubCategory_EServiceCategory_EServiceCategoryId",
                schema: "lookup",
                table: "EServiceSubCategory",
                column: "EServiceCategoryId",
                principalSchema: "lookup",
                principalTable: "EServiceCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UploadRequest_EServiceSubCategory_EServiceSubCategoryId",
                schema: "services",
                table: "UploadRequest",
                column: "EServiceSubCategoryId",
                principalSchema: "lookup",
                principalTable: "EServiceSubCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
