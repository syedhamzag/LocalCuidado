using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_Client_HeartRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Client_HeartRate",
                columns: table => new
                {
                    HeartRateId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reference = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    TargetHR = table.Column<int>(nullable: false),
                    TargetHRAttach = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    GenderAttach = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    BeatsPerSeconds = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: false),
                    SeeChart = table.Column<int>(nullable: false),
                    SeeChartAttach = table.Column<string>(nullable: true),
                    PhysicianResponse = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_HeartRate", x => x.HeartRateId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_HeartRate_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });
                migrationBuilder.CreateIndex(
                    name: "IX_tbl_Client_HeartRate_HeartRateId",
                    table: "tbl_Client_HeartRate",
                    column: "HeartRateId");
                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Client_HeartRate");
        }
    }
}
