using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientPulseRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientPulseRate",
                columns: table => new
                {
                    PulseRateId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reference = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    TargetPulse = table.Column<int>(nullable: false),
                    TargetPulseAttach = table.Column<string>(nullable: true),
                    CurrentPulse = table.Column<string>(nullable: false),
                    Chart = table.Column<string>(nullable: true),
                    SeeChart = table.Column<int>(nullable: false),
                    SeeChartAttach = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: false),
                    PhysicianResponse = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientPulseRate", x => x.PulseRateId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientPulseRate_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });
                migrationBuilder.CreateIndex(
                    name: "IX_tbl_ClientPulseRate_PulseRateId",
                    table: "tbl_ClientPulseRate",
                    column: "PulseRateId");
                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientPulseRate");
        }
    }
}
