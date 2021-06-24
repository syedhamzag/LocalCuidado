using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Multi_Complain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Complain_OfficerToAct",
                columns: table => new
                {
                    ComplainOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    ComplainId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Complain_OfficerToAct", x => x.ComplainOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_Complain_OfficerToAct_tbl_Client_ComplainRegister_ComplainId",
                        column: x => x.ComplainId,
                        principalTable: "tbl_Client_ComplainRegister",
                        principalColumn: "ComplainId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Complain_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Complain_StaffName",
                columns: table => new
                {
                    ComplainStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    ComplainId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Complain_StaffName", x => x.ComplainStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_Complain_StaffName_tbl_Client_ComplainRegister_ComplainId",
                        column: x => x.ComplainId,
                        principalTable: "tbl_Client_ComplainRegister",
                        principalColumn: "ComplainId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Complain_StaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);

                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Complain_OfficerToAct_ComplainId",
                table: "tbl_Complain_OfficerToAct",
                column: "ComplainId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ComplainStaffId_ComplainId",
                table: "tbl_Complain_StaffName",
                column: "ComplainId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Complain_OfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_Complain_StaffName");
        }
    }
}
