using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffMedCompObs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffMedCompObs",
                columns: table => new
                {
                    MedCompId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NextCheckDate = table.Column<DateTime>(nullable: false),
                    Details = table.Column<string>(maxLength: 255, nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    UnderstandingofMedication = table.Column<int>(nullable: false),
                    UnderstandingofRights = table.Column<int>(nullable: false),
                    ReadingMedicalPrescriptions = table.Column<int>(nullable: false),
                    CarePlan = table.Column<int>(nullable: false),
                    RateStaff = table.Column<int>(nullable: false),
                    OfficerToAct = table.Column<int>(nullable: false),
                    ActionRequired = table.Column<string>(maxLength: 255, nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 255, nullable: false),
                    URL = table.Column<string>(maxLength: 255, nullable: false),
                    Attachment = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffMedCompObs", x => x.MedCompId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffMedCompObs_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_StaffMedCompObs_tbl_StaffPersonalInfo_StaffId",
                        column: x => x.StaffId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffMedCompObs_MedCompId",
                table: "tbl_StaffMedCompObs",
                column: "MedCompId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffMedCompObs");
        }
    }
}