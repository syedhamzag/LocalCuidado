using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffIncidentReporting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffIncidentReporting",
                columns: table => new
                {
                    StaffIncidentReportingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportingStaffId = table.Column<int>(nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    StaffInvolvedId = table.Column<int>(nullable: false),
                    IncidentType = table.Column<int>(nullable: false),
                    IncidentDetails = table.Column<string>(nullable: false),
                    ActionTaken = table.Column<string>(maxLength: 250, nullable: true),
                    Witness = table.Column<string>(nullable: true),
                    Attachment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffIncidentReporting", x => x.StaffIncidentReportingId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffIncidentReporting");
        }
    }
}
