using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class MultiLogAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_LogAuditOfficerToAct",
                columns: table => new
                {
                    LogAuditOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    LogAuditId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_LogAuditOfficerToAct", x => x.LogAuditOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_LogAuditOfficerToAct_tbl_ClientLogAudit_LogAuditId",
                        column: x => x.LogAuditId,
                        principalTable: "tbl_ClientLogAudit",
                        principalColumn: "LogAuditId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_LogAuditOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_LogAuditOfficerToAct_LogAuditId",
                table: "tbl_LogAuditOfficerToAct",
                column: "LogAuditId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_LogAuditOfficerToAct");

        }
    }
}
