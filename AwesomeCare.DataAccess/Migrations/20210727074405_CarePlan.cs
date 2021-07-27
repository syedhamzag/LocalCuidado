using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class CarePlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Capacity",
                columns: table => new
                {
                    CapacityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    Pointer = table.Column<int>(nullable: false),
                    Implications = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Capacity", x => x.CapacityId);
                    table.ForeignKey(
                        name: "FK_tbl_Capacity_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ConsentCare",
                columns: table => new
                {
                    CareId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    Signature = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ConsentCare", x => x.CareId);
                    table.ForeignKey(
                        name: "FK_tbl_ConsentCare_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ConsentData",
                columns: table => new
                {
                    DataId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    Signature = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ConsentData", x => x.DataId);
                    table.ForeignKey(
                        name: "FK_tbl_ConsentData_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ConsentLandLine",
                columns: table => new
                {
                    LandlineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    LogMethod = table.Column<int>(nullable: false),
                    Signature = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ConsentLandLine", x => x.LandlineId);
                    table.ForeignKey(
                        name: "FK_tbl_ConsentLandLine_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Equipment",
                columns: table => new
                {
                    EquipmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    Name = table.Column<int>(nullable: false),
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
                        name: "FK_tbl_Equipment_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Equipment_tbl_StaffPersonalInfo_PersonToAct",
                        column: x => x.PersonToAct,
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
                    ClientId = table.Column<int>(nullable: false),
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
                        name: "FK_tbl_KeyIndicators_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Personal",
                columns: table => new
                {
                    PersonalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    Smoking = table.Column<int>(nullable: false),
                    DNR = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Personal", x => x.PersonalId);
                    table.ForeignKey(
                        name: "FK_tbl_Personal_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PersonCentred",
                columns: table => new
                {
                    PersonCentredId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    Class = table.Column<int>(nullable: false),
                    ExpSupport = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PersonCentred", x => x.PersonCentredId);
                    table.ForeignKey(
                        name: "FK_tbl_PersonCentred_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Review",
                columns: table => new
                {
                    ReviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    CP_PreDate = table.Column<DateTime>(nullable: false),
                    CP_ReviewDate = table.Column<DateTime>(nullable: false),
                    RA_PreDate = table.Column<DateTime>(nullable: false),
                    RA_ReviewDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Review", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_tbl_Review_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_CapacityIndicator",
                columns: table => new
                {
                    CapacityIndicatorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CapacityId = table.Column<int>(nullable: false),
                    BaseRecordId = table.Column<int>(nullable: false)
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
                    PersonCentredId = table.Column<int>(nullable: false),
                    BaseRecordId = table.Column<int>(nullable: false)
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
                name: "IX_tbl_Capacity_ClientId",
                table: "tbl_Capacity",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_CapacityIndicator_CapacityId",
                table: "tbl_CapacityIndicator",
                column: "CapacityId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ConsentCare_ClientId",
                table: "tbl_ConsentCare",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ConsentData_ClientId",
                table: "tbl_ConsentData",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ConsentLandLine_ClientId",
                table: "tbl_ConsentLandLine",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Equipment_ClientId",
                table: "tbl_Equipment",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Equipment_PersonToAct",
                table: "tbl_Equipment",
                column: "PersonToAct");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_KeyIndicators_ClientId",
                table: "tbl_KeyIndicators",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Personal_ClientId",
                table: "tbl_Personal",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PersonCentred_ClientId",
                table: "tbl_PersonCentred",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PersonCentredFocus_PersonCentredId",
                table: "tbl_PersonCentredFocus",
                column: "PersonCentredId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Review_ClientId",
                table: "tbl_Review",
                column: "ClientId");
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
        }
    }
}
