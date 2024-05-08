using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QassimPrincipality.Infrastructure.Migrations
{
    public partial class someUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OpenDataRequestId",
                schema: "lookup",
                table: "Attachment",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_OpenDataRequestId",
                schema: "lookup",
                table: "Attachment",
                column: "OpenDataRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_OpenDataRequest_OpenDataRequestId",
                schema: "lookup",
                table: "Attachment",
                column: "OpenDataRequestId",
                principalSchema: "services",
                principalTable: "OpenDataRequest",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_OpenDataRequest_OpenDataRequestId",
                schema: "lookup",
                table: "Attachment");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_OpenDataRequestId",
                schema: "lookup",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "OpenDataRequestId",
                schema: "lookup",
                table: "Attachment");
        }
    }
}
