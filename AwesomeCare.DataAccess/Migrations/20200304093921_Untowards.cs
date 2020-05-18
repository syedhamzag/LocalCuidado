using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Untowards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Untowards",
                columns: table => new
                {
                    UntowardsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TicketNumber = table.Column<string>(maxLength: 50, nullable: false),
                    Date = table.Column<string>(maxLength: 15, nullable: false),
                    Subject = table.Column<string>(maxLength: 225, nullable: false),
                    TimeOfCall = table.Column<string>(maxLength: 15, nullable: false),
                    PersonReporting = table.Column<string>(maxLength: 100, nullable: true),
                    PersonReportingTelephone = table.Column<string>(maxLength: 50, nullable: true),
                    PersonReportingEmail = table.Column<string>(maxLength: 225, nullable: true),
                    Details = table.Column<string>(maxLength: 225, nullable: false),
                    ActionStatus = table.Column<string>(maxLength: 7, nullable: false),
                    Priority = table.Column<string>(maxLength: 7, nullable: false),
                    ActionTaken = table.Column<string>(maxLength: 225, nullable: false),
                    ActionRequired = table.Column<string>(maxLength: 225, nullable: false),
                    FinalActionToCloseCase = table.Column<string>(maxLength: 225, nullable: true),
                    ExpectedDateAndTimeOfFeedback = table.Column<string>(maxLength: 225, nullable: false),
                    IsBlackListRequired = table.Column<bool>(nullable: false),
                    HomeCareClientId = table.Column<int>(nullable: false),
                    IsHospitalEntry = table.Column<bool>(nullable: false),
                    HospitalEntryReason = table.Column<string>(maxLength: 225, nullable: false),
                    IsHospitalExit = table.Column<bool>(nullable: false),
                    HospitalExitDetails = table.Column<string>(maxLength: 225, nullable: false),
                    TypeofRequiredNotification = table.Column<int>(nullable: false),
                    ShouldNotifyInvolvingStaff = table.Column<bool>(nullable: false),
                    Attachment = table.Column<string>(nullable: false),
                    Others = table.Column<string>(maxLength: 225, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Untowards", x => x.UntowardsId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UntowardsOfficerToAct",
                columns: table => new
                {
                    UntowardsOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    UntowardsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UntowardsOfficerToAct", x => x.UntowardsOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_UntowardsOfficerToAct_tbl_Untowards_UntowardsId",
                        column: x => x.UntowardsId,
                        principalTable: "tbl_Untowards",
                        principalColumn: "UntowardsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UntowardsStaffInvolved",
                columns: table => new
                {
                    UntowardsStaffInvolvedId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    UntowardsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UntowardsStaffInvolved", x => x.UntowardsStaffInvolvedId);
                    table.ForeignKey(
                        name: "FK_tbl_UntowardsStaffInvolved_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_UntowardsStaffInvolved_tbl_Untowards_UntowardsId",
                        column: x => x.UntowardsId,
                        principalTable: "tbl_Untowards",
                        principalColumn: "UntowardsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UntowardsOfficerToAct_UntowardsId",
                table: "tbl_UntowardsOfficerToAct",
                column: "UntowardsId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UntowardsStaffInvolved_StaffPersonalInfoId",
                table: "tbl_UntowardsStaffInvolved",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UntowardsStaffInvolved_UntowardsId",
                table: "tbl_UntowardsStaffInvolved",
                column: "UntowardsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_UntowardsOfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_UntowardsStaffInvolved");

            migrationBuilder.DropTable(
                name: "tbl_Untowards");
        }
    }
}
