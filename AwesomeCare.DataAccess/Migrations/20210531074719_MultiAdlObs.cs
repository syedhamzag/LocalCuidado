using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class MultiAdlObs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_AdlObsOfficerToAct",
                columns: table => new
                {
                    AdlObsOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    ObservationID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_AdlObsOfficerToAct", x => x.AdlObsOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_AdlObsOfficerToAct_tbl_ClientAdlObs_ObservationID",
                        column: x => x.ObservationID,
                        principalTable: "tbl_ClientAdlObs",
                        principalColumn: "ObservationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_AdlObsOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_tbl_AdlObsOfficerToAct_ObservationID",
                table: "tbl_AdlObsOfficerToAct",
                column: "ObservationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_AdlObsOfficerToAct");
        }
    }
}
