using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QassimPrincipality.Infrastructure.Migrations
{
    public partial class EditEservicesTblData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE [lookup].[EService] SET HasApplicantStatus = 1 WHERE Id = 5");
            migrationBuilder.Sql("UPDATE [lookup].[EService] SET HasApplicantStatus = 1 WHERE Id = 6");
            migrationBuilder.Sql("UPDATE [lookup].[EService] SET HasApplicantStatus = 1 WHERE Id = 7");
            migrationBuilder.Sql("UPDATE [lookup].[EService] SET HasApplicantStatus = 1 WHERE Id = 8");
            migrationBuilder.Sql("UPDATE [lookup].[EService] SET HasApplicantStatus = 1 WHERE Id = 9");
            migrationBuilder.Sql("UPDATE [lookup].[EService] SET HasTypeOfSummons = 1 WHERE Id = 10");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
