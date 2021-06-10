using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffSupervisionAppraisal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffSupervisionAppraisal",
                columns: table => new
                {
                    StaffSupervisionAppraisalId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NextCheckDate = table.Column<DateTime>(nullable: false),
                    Details = table.Column<string>(maxLength: 255, nullable: false),
                    WorkTeam = table.Column<int>(nullable: false),
                    StaffRating = table.Column<int>(nullable: false),
                    ProfessionalDevelopment = table.Column<int>(nullable: false),
                    StaffComplaints = table.Column<int>(nullable: false),
                    FiveStarRating = table.Column<string>(maxLength: 255, nullable: false),
                    StaffDevelopment = table.Column<string>(maxLength: 255, nullable: false),
                    StaffAbility = table.Column<string>(maxLength: 255, nullable: false),
                    NoAbilityToSupport = table.Column<string>(maxLength: 255, nullable: false),
                    CondourAndWhistleBlowing = table.Column<string>(maxLength: 255, nullable: false),
                    NoCondourAndWhistleBlowing = table.Column<string>(maxLength: 255, nullable: false),
                    StaffSupportAreas = table.Column<int>(nullable: false),
                    ActionRequired = table.Column<string>(maxLength: 255, nullable: false),
                    OfficerToAct = table.Column<int>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 255, nullable: false),
                    URL = table.Column<string>(maxLength: 255, nullable: false),
                    Attachment = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffSupervisionAppraisal", x => x.StaffSupervisionAppraisalId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffSupervisionAppraisal_tbl_StaffPersonalInfo_StaffId",
                        column: x => x.StaffId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffSupervisionAppraisal_StaffSupervisionAppraisalId",
                table: "tbl_StaffSupervisionAppraisal",
                column: "StaffSupervisionAppraisalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffSupervisionAppraisal");
        }
    }
}
