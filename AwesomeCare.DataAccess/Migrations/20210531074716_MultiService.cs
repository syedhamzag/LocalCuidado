using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class MultiService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ServiceOfficerToAct",
                columns: table => new
                {
                    ServiceOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    WatchId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ServiceOfficerToAct", x => x.ServiceOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_ServiceOfficerToAct_tbl_ClientServiceWatch_WatchId",
                        column: x => x.WatchId,
                        principalTable: "tbl_ClientServiceWatch",
                        principalColumn: "WatchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ServiceOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ServiceStaffName",
                columns: table => new
                {
                    ServiceStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    WatchId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ServicePersonInvolved", x => x.ServiceStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_ServiceStaffName_tbl_ClientServiceWatch_WatchId",
                        column: x => x.WatchId,
                        principalTable: "tbl_ClientServiceWatch",
                        principalColumn: "WatchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ServiceStaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);

                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ServiceOfficerToAct_WatchId",
                table: "tbl_ServiceOfficerToAct",
                column: "WatchId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ServiceStaffName_WatchId",
                table: "tbl_ServiceStaffName",
                column: "WatchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ServiceOfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_ServiceStaffName");
        }
    }
}
