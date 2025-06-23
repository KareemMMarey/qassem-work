using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QassimPrincipality.Infrastructure.Migrations
{
    public partial class CompleteRequestAttType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT [lookup].[AttachmentType] ON 
                GO

                INSERT
                    [lookup].[AttachmentType] (
                        [Id],
                        [IsMandatory],
                        [MaxSizeMB],
                        [AllowedExtensions],
                        [ServiceId],
                        [CreatedBy],
                        [CreatedOn],
                        [UpdatedBy],
                        [UpdatedOn],
                        [NameAr],
                        [NameEn],
                        [IsActive],
                        [DescriptionAr],
                        [DescriptionEn]
                    )

                select 
                        54+id,
                        1,
                        1024,
                        N'pdf,jpg,png',
                        id,
                        N'System',
                        CAST(N'2025-05-18T19:07:48.5900000' AS DateTime2),
                        N'System',
                        CAST(N'2025-05-18T19:07:48.5900000' AS DateTime2),
                        N'مستندات اكمال الطلب',
                        N'Application completion documents',
                        1,
                        N'مستندات اكمال الطلب',
                        N'Application completion documents'
    
	                from [lookup].[EService]
	                go

                SET IDENTITY_INSERT [lookup].[AttachmentType] OFF
                GO
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
