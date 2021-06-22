using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class MultiSeizure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_SeizureOfficerToAct",
                columns: table => new
                {
                    SeizureOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    SeizureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SeizureOfficerToAct", x => x.SeizureOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_SeizureOfficerToAct_tbl_ClientSeizure_SeizureId",
                        column: x => x.SeizureId,
                        principalTable: "tbl_ClientSeizure",
                        principalColumn: "SeizureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_SeizureOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_SeizureStaffName",
                columns: table => new
                {
                    SeizureStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    SeizureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SeizureStaffInvolved", x => x.SeizureStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_SeizureStaffName_tbl_ClientSeizure_SeizureId",
                        column: x => x.SeizureId,
                        principalTable: "tbl_ClientSeizure",
                        principalColumn: "SeizureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_SeizureStaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_SeizurePhysician",
                columns: table => new
                {
                    SeizurePhysicianId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    SeizureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SeizurePhysician", x => x.SeizurePhysicianId);
                    table.ForeignKey(
                        name: "FK_tbl_SeizurePhysician_tbl_ClientSeizure_SeizureId",
                        column: x => x.SeizureId,
                        principalTable: "tbl_ClientSeizure",
                        principalColumn: "SeizureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_SeizurePhysician_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_SeizureOfficerToAct_SeizureId",
                table: "tbl_SeizureOfficerToAct",
                column: "SeizureId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_SeizurePhysician_SeizureId",
                table: "tbl_SeizurePhysician",
                column: "SeizureId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_SeizureStaffName_SeizureId",
                table: "tbl_SeizureStaffName",
                column: "SeizureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_SeizureOfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_SeizurePhysician");
            migrationBuilder.DropTable(
                name: "tbl_SeizureStaffName");
        }
    }
}
