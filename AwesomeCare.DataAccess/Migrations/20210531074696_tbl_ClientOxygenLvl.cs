using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientOxygenLvl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientOxygenlvl",
                columns: table => new
                {
                    OxygenlvlId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reference = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    TargetOxygen = table.Column<int>(nullable: false),
                    TargetOxygenAttach = table.Column<string>(nullable: true),
                    CurrentReading = table.Column<string>(nullable: false),
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
                    table.PrimaryKey("PK_tbl_ClientOxygenlvl", x => x.OxygenlvlId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientOxygenlvl_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientOxygenlvl_OxygenlvlId",
                table: "tbl_ClientOxygenlvl",
                column: "OxygenlvlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientOxygenlvl");
        }
    }
}
