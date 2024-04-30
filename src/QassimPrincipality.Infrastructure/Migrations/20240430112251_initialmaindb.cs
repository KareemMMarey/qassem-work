using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QassimPrincipality.Infrastructure.Migrations
{
    public partial class initialmaindb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "lookup");

            migrationBuilder.EnsureSchema(
                name: "services");

            migrationBuilder.CreateTable(
                name: "Classification",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactType",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EServiceCategory",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EServiceCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestType",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceEvaluation",
                schema: "services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    EvalutionValue = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceEvaluation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactForm",
                schema: "services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactForm_ContactType_ContactTypeId",
                        column: x => x.ContactTypeId,
                        principalSchema: "lookup",
                        principalTable: "ContactType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EServiceSubCategory",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EServiceCategoryId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EServiceSubCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EServiceSubCategory_EServiceCategory_EServiceCategoryId",
                        column: x => x.EServiceCategoryId,
                        principalSchema: "lookup",
                        principalTable: "EServiceCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UploadRequest",
                schema: "services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    referralNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestSubClassificationId = table.Column<int>(type: "int", nullable: false),
                    OriginalRequestId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestTypeId = table.Column<int>(type: "int", nullable: false),
                    EServiceSubCategoryId = table.Column<int>(type: "int", nullable: false),
                    OriginatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequestOwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequestOwnerNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExecutiveSummary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RequestNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UploadRequest_EServiceSubCategory_EServiceSubCategoryId",
                        column: x => x.EServiceSubCategoryId,
                        principalSchema: "lookup",
                        principalTable: "EServiceSubCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UploadRequest_RequestType_RequestTypeId",
                        column: x => x.RequestTypeId,
                        principalSchema: "lookup",
                        principalTable: "RequestType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsArabic = table.Column<bool>(type: "bit", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttachmentType = table.Column<int>(type: "int", nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOpenSourceArabic = table.Column<bool>(type: "bit", nullable: true),
                    IsOpenSourceEnglish = table.Column<bool>(type: "bit", nullable: true),
                    IsCloseSourceArabic = table.Column<bool>(type: "bit", nullable: true),
                    IsCloseSourceEnglish = table.Column<bool>(type: "bit", nullable: true),
                    IsData = table.Column<bool>(type: "bit", nullable: true),
                    IsSupporting = table.Column<bool>(type: "bit", nullable: true),
                    IsSanitizedDocument = table.Column<bool>(type: "bit", nullable: true),
                    UploadRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachment_UploadRequest_UploadRequestId",
                        column: x => x.UploadRequestId,
                        principalSchema: "services",
                        principalTable: "UploadRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AttachmentContent",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Thumbnail = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    AttachmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttachmentContent_Attachment_AttachmentId",
                        column: x => x.AttachmentId,
                        principalSchema: "lookup",
                        principalTable: "Attachment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_UploadRequestId",
                schema: "lookup",
                table: "Attachment",
                column: "UploadRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentContent_AttachmentId",
                schema: "lookup",
                table: "AttachmentContent",
                column: "AttachmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactForm_ContactTypeId",
                schema: "services",
                table: "ContactForm",
                column: "ContactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EServiceSubCategory_EServiceCategoryId",
                schema: "lookup",
                table: "EServiceSubCategory",
                column: "EServiceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadRequest_EServiceSubCategoryId",
                schema: "services",
                table: "UploadRequest",
                column: "EServiceSubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadRequest_RequestTypeId",
                schema: "services",
                table: "UploadRequest",
                column: "RequestTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttachmentContent",
                schema: "lookup");

            migrationBuilder.DropTable(
                name: "Classification",
                schema: "lookup");

            migrationBuilder.DropTable(
                name: "ContactForm",
                schema: "services");

            migrationBuilder.DropTable(
                name: "ServiceEvaluation",
                schema: "services");

            migrationBuilder.DropTable(
                name: "Attachment",
                schema: "lookup");

            migrationBuilder.DropTable(
                name: "ContactType",
                schema: "lookup");

            migrationBuilder.DropTable(
                name: "UploadRequest",
                schema: "services");

            migrationBuilder.DropTable(
                name: "EServiceSubCategory",
                schema: "lookup");

            migrationBuilder.DropTable(
                name: "RequestType",
                schema: "lookup");

            migrationBuilder.DropTable(
                name: "EServiceCategory",
                schema: "lookup");
        }
    }
}
