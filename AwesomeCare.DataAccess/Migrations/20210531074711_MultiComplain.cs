using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class MultiComplain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ComplainOfficerToAct",
                columns: table => new
                {
                    ComplainOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    ComplainId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ComplainOfficerToAct", x => x.ComplainOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_ComplainOfficerToAct_tbl_ClientComplainRegister_ComplainId",
                        column: x => x.ComplainId,
                        principalTable: "tbl_ClientComplainRegister",
                        principalColumn: "ComplainId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ComplainOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ComplainStaffName",
                columns: table => new
                {
                    ComplainStaffNameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    ComplainId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ComplainStaffName", x => x.ComplainStaffNameId);
                    table.ForeignKey(
                        name: "FK_tbl_ComplainStaffName_tbl_ClientComplainRegister_ComplainId",
                        column: x => x.ComplainId,
                        principalTable: "tbl_ClientComplainRegister",
                        principalColumn: "ComplainId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ComplainStaffName_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);

                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ComplainOfficerToAct_ComplainId",
                table: "tbl_ComplainOfficerToAct",
                column: "ComplainId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ComplainStaffId_ComplainId",
                table: "tbl_ComplainStaffName",
                column: "ComplainId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ComplainOfficerToAct");
            migrationBuilder.DropTable(
                name: "tbl_ComplainStaffName");
        }
    }
}
