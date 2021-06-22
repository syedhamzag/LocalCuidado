using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientBodyTemp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientBodyTemp",
                columns: table => new
                {
                    BodyTempId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reference = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    TargetTemp = table.Column<int>(nullable: false),
                    TargetTempAttach = table.Column<string>(nullable: true),
                    CurrentReading = table.Column<string>(nullable: false),
                    SeeChart = table.Column<int>(nullable: false),
                    SeeChartAttach = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: false),
                    StaffName = table.Column<int>(nullable: false),
                    Physician = table.Column<int>(nullable: false),
                    PhysicianResponse = table.Column<string>(nullable: false),
                    OfficerToAct = table.Column<int>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientBodyTemp", x => x.BodyTempId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientBodyTemp_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });
                migrationBuilder.CreateIndex(
                    name: "IX_tbl_ClientBodyTemp_BodyTempId",
                    table: "tbl_ClientBodyTemp",
                    column: "BodyTempId");
                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientBodyTemp");
        }
    }
}
