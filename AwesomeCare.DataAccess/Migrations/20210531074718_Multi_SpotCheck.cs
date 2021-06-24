using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Multi_SpotCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_SpotCheck_OfficerToAct",
                columns: table => new
                {
                    SpotCheckOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    SpotCheckId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SpotCheck_OfficerToAct", x => x.SpotCheckOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_SpotCheck_OfficerToAct_tbl_ClientSpotCheck_SpotCheckId",
                        column: x => x.SpotCheckId,
                        principalTable: "tbl_ClientSpotCheck",
                        principalColumn: "SpotCheckId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_SpotCheck_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_SpotCheck_OfficerToAct_SpotCheckId",
                table: "tbl_SpotCheck_OfficerToAct",
                column: "SpotCheckId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_SpotCheck_OfficerToAct");
        }
    }
}
