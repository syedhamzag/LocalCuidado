using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class MultiMedComp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_MedCompOfficerToAct",
                columns: table => new
                {
                    MedCompOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    MedCompId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_MedCompOfficerToAct", x => x.MedCompOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_MedCompOfficerToAct_tbl_ClientMedComp_MedCompId",
                        column: x => x.MedCompId,
                        principalTable: "tbl_ClientMedComp",
                        principalColumn: "MedCompId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_MedCompOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_MedCompOfficerToAct_MedCompId",
                table: "tbl_MedCompOfficerToAct",
                column: "MedCompId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_MedCompOfficerToAct");
        }
    }
}
