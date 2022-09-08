using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class IncidentReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_IncidentReporting",
                columns: table => new
                {
                    IncidentReportingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportingStaffId = table.Column<int>(nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    StaffInvolvedId = table.Column<int>(nullable: false),
                    IncidentTypeId = table.Column<int>(nullable: false),
                    IncidentDetails = table.Column<string>(nullable: false),
                    ActionTaken = table.Column<string>(maxLength: 250, nullable: true),
                    Witness = table.Column<string>(nullable: true),
                    Attachment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_IncidentReporting", x => x.IncidentReportingId);
                    table.ForeignKey(
                        name: "FK_tbl_IncidentReporting_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_IncidentReporting_ClientId",
                table: "tbl_IncidentReporting",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_IncidentReporting");
        }
    }
}
