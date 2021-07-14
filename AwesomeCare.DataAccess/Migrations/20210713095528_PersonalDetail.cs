using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class PersonalDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_PersonalDetail",
                columns: table => new
                {
                    PersonalDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PersonalDetail", x => x.PersonalDetailId);
                    table.ForeignKey(
                        name: "FK_tbl_PersonalDetail_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Capacity",
                columns: table => new
                {
                    CapacityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalDetailId = table.Column<int>(nullable: false),
                    Pointer = table.Column<int>(nullable: false),
                    Implications = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Capacity", x => x.CapacityId);
                    table.ForeignKey(
                        name: "FK_tbl_Capacity_tbl_PersonalDetail_PersonalDetailId",
                        column: x => x.PersonalDetailId,
                        principalTable: "tbl_PersonalDetail",
                        principalColumn: "PersonalDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ConsentCare",
                columns: table => new
                {
                    CareId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalDetailId = table.Column<int>(nullable: false),
                    Signature = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ConsentCare", x => x.CareId);
                    table.ForeignKey(
                        name: "FK_tbl_ConsentCare_tbl_PersonalDetail_PersonalDetailId",
                        column: x => x.PersonalDetailId,
                        principalTable: "tbl_PersonalDetail",
                        principalColumn: "PersonalDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ConsentData",
                columns: table => new
                {
                    DataId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalDetailId = table.Column<int>(nullable: false),
                    Signature = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ConsentData", x => x.DataId);
                    table.ForeignKey(
                        name: "FK_tbl_ConsentData_tbl_PersonalDetail_PersonalDetailId",
                        column: x => x.PersonalDetailId,
                        principalTable: "tbl_PersonalDetail",
                        principalColumn: "PersonalDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ConsentLandLine",
                columns: table => new
                {
                    LandlineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalDetailId = table.Column<int>(nullable: false),
                    LogMethod = table.Column<int>(nullable: false),
                    Signature = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ConsentLandLine", x => x.LandlineId);
                    table.ForeignKey(
                        name: "FK_tbl_ConsentLandLine_tbl_PersonalDetail_PersonalDetailId",
                        column: x => x.PersonalDetailId,
                        principalTable: "tbl_PersonalDetail",
                        principalColumn: "PersonalDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Equipment",
                columns: table => new
                {
                    EquipmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalDetailId = table.Column<int>(nullable: false),
                    Name = table.Column<int>(nullable: false),
                    StaffId = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Location = table.Column<int>(nullable: false),
                    ServiceDate = table.Column<DateTime>(nullable: false),
                    NextServiceDate = table.Column<DateTime>(nullable: false),
                    Attachment = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    PersonToAct = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Equipment", x => x.EquipmentId);
                    table.ForeignKey(
                        name: "FK_tbl_Equipment_tbl_PersonalDetail_PersonalDetailId",
                        column: x => x.PersonalDetailId,
                        principalTable: "tbl_PersonalDetail",
                        principalColumn: "PersonalDetailId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Equipment_tbl_StaffPersonalInfo_StaffId",
                        column: x => x.StaffId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_KeyIndicators",
                columns: table => new
                {
                    KeyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalDetailId = table.Column<int>(nullable: false),
                    AboutMe = table.Column<string>(nullable: false),
                    FamilyRole = table.Column<string>(nullable: false),
                    LivingStatus = table.Column<int>(nullable: false),
                    Debture = table.Column<int>(nullable: false),
                    ThingsILike = table.Column<string>(nullable: false),
                    LogMethod = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_KeyIndicators", x => x.KeyId);
                    table.ForeignKey(
                        name: "FK_tbl_KeyIndicators_tbl_PersonalDetail_PersonalDetailId",
                        column: x => x.PersonalDetailId,
                        principalTable: "tbl_PersonalDetail",
                        principalColumn: "PersonalDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Personal",
                columns: table => new
                {
                    PersonalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalDetailId = table.Column<int>(nullable: false),
                    Smoking = table.Column<int>(nullable: false),
                    DNR = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Personal", x => x.PersonalId);
                    table.ForeignKey(
                        name: "FK_tbl_Personal_tbl_PersonalDetail_PersonalDetailId",
                        column: x => x.PersonalDetailId,
                        principalTable: "tbl_PersonalDetail",
                        principalColumn: "PersonalDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PersonCentred",
                columns: table => new
                {
                    PersonCentredId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalDetailId = table.Column<int>(nullable: false),
                    Signature = table.Column<int>(nullable: false),
                    Date = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PersonCentred", x => x.PersonCentredId);
                    table.ForeignKey(
                        name: "FK_tbl_PersonCentred_tbl_PersonalDetail_PersonalDetailId",
                        column: x => x.PersonalDetailId,
                        principalTable: "tbl_PersonalDetail",
                        principalColumn: "PersonalDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Review",
                columns: table => new
                {
                    ReviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalDetailId = table.Column<int>(nullable: false),
                    CP_PreDate = table.Column<DateTime>(nullable: false),
                    CP_ReviewDate = table.Column<DateTime>(nullable: false),
                    RA_PreDate = table.Column<DateTime>(nullable: false),
                    RA_ReviewDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Review", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_tbl_Review_tbl_PersonalDetail_PersonalDetailId",
                        column: x => x.PersonalDetailId,
                        principalTable: "tbl_PersonalDetail",
                        principalColumn: "PersonalDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_CapacityIndicator",
                columns: table => new
                {
                    CapacityIndicatorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CapacityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_CapacityIndicator", x => x.CapacityIndicatorId);
                    table.ForeignKey(
                        name: "FK_tbl_CapacityIndicator_tbl_Capacity_CapacityId",
                        column: x => x.CapacityId,
                        principalTable: "tbl_Capacity",
                        principalColumn: "CapacityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PersonCentredFocus",
                columns: table => new
                {
                    PersonCentredFocusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonCentredId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PersonCentredFocus", x => x.PersonCentredFocusId);
                    table.ForeignKey(
                        name: "FK_tbl_PersonCentredFocus_tbl_PersonCentred_PersonCentredId",
                        column: x => x.PersonCentredId,
                        principalTable: "tbl_PersonCentred",
                        principalColumn: "PersonCentredId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Capacity_PersonalDetailId",
                table: "tbl_Capacity",
                column: "PersonalDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_CapacityIndicator_CapacityId",
                table: "tbl_CapacityIndicator",
                column: "CapacityId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ConsentCare_PersonalDetailId",
                table: "tbl_ConsentCare",
                column: "PersonalDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ConsentData_PersonalDetailId",
                table: "tbl_ConsentData",
                column: "PersonalDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ConsentLandLine_PersonalDetailId",
                table: "tbl_ConsentLandLine",
                column: "PersonalDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Equipment_PersonalDetailId",
                table: "tbl_Equipment",
                column: "PersonalDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Equipment_StaffId",
                table: "tbl_Equipment",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_KeyIndicators_PersonalDetailId",
                table: "tbl_KeyIndicators",
                column: "PersonalDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Personal_PersonalDetailId",
                table: "tbl_Personal",
                column: "PersonalDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PersonalDetail_ClientId",
                table: "tbl_PersonalDetail",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PersonCentred_PersonalDetailId",
                table: "tbl_PersonCentred",
                column: "PersonalDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PersonCentredFocus_PersonCentredId",
                table: "tbl_PersonCentredFocus",
                column: "PersonCentredId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Review_PersonalDetailId",
                table: "tbl_Review",
                column: "PersonalDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_CapacityIndicator");

            migrationBuilder.DropTable(
                name: "tbl_ConsentCare");

            migrationBuilder.DropTable(
                name: "tbl_ConsentData");

            migrationBuilder.DropTable(
                name: "tbl_ConsentLandLine");

            migrationBuilder.DropTable(
                name: "tbl_Equipment");

            migrationBuilder.DropTable(
                name: "tbl_KeyIndicators");

            migrationBuilder.DropTable(
                name: "tbl_Personal");

            migrationBuilder.DropTable(
                name: "tbl_PersonCentredFocus");

            migrationBuilder.DropTable(
                name: "tbl_Review");

            migrationBuilder.DropTable(
                name: "tbl_Capacity");

            migrationBuilder.DropTable(
                name: "tbl_PersonCentred");

            migrationBuilder.DropTable(
                name: "tbl_PersonalDetail");
        }
    }
}
