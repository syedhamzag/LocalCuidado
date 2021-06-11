using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientBloodPressure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientBloodPressure",
                columns: table => new
                {
                    BloodPressureId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    GoalSystolic = table.Column<int>(nullable: false),
                    GoalDiastolic = table.Column<int>(nullable: false),
                    ReadingSystolic = table.Column<int>(nullable: false),
                    ReadingDiastolic = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(maxLength: 255, nullable: false),
                    StaffName = table.Column<int>(nullable: false),
                    Physician = table.Column<int>(nullable: false),
                    PhysicianResponse = table.Column<string>(maxLength: 255, nullable: false),
                    OfficerToAct = table.Column<int>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 255, nullable: false),
                    SecondStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientBloodPressure", x => x.BloodPressureId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientBloodPressure_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ClientBloodPressure_tbl_StaffPersonalInfo_OfficerToAct",
                        column: x => x.OfficerToAct,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientBloodPressure");
        }
    }
}
