using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_Client_BloodPressure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Client_BloodPressure",
                columns: table => new
                {
                    BloodPressureId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reference = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    GoalSystolic = table.Column<int>(nullable: false),
                    GoalDiastolic = table.Column<int>(nullable: false),
                    ReadingSystolic = table.Column<int>(nullable: false),
                    ReadingDiastolic = table.Column<int>(nullable: false),
                    StatusImage = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: false),
                    PhysicianResponse = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    StatusAttach = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_BloodPressure", x => x.BloodPressureId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_BloodPressure_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });
                migrationBuilder.CreateIndex(
                    name: "IX_tbl_Client_BloodPressure_BloodPressureId",
                    table: "tbl_Client_BloodPressure",
                    column: "BloodPressureId");
                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Client_BloodPressure");
        }
    }
}
