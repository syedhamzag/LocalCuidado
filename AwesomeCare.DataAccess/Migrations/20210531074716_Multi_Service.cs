using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Multi_Service : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Service_OfficerToAct",
                columns: table => new
                {
                    ServiceOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    WatchId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Service_OfficerToAct", x => x.ServiceOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_Service_OfficerToAct_tbl_Client_ServiceWatch_WatchId",
                        column: x => x.WatchId,
                        principalTable: "tbl_Client_ServiceWatch",
                        principalColumn: "WatchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Service_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Service_StaffName",
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
                        name: "FK_tbl_Service_StaffName_tbl_Client_ServiceWatch_WatchId",
                        column: x => x.WatchId,
                        principalTable: "tbl_Client_ServiceWatch",
                        principalColumn: "WatchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Service_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);

                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Service_OfficerToAct_WatchId",
                table: "tbl_Service_OfficerToAct",
                column: "WatchId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Service_StaffName_WatchId",
                table: "tbl_Service_StaffName",
                column: "WatchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Service_OfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_Service_StaffName");
        }
    }
}
