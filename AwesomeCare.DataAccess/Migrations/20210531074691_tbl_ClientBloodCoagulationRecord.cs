using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientBloodCoagulationRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientBloodCoagulationRecord",
                columns: table => new
                {
                    BloodRecordId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reference = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Indication = table.Column<int>(nullable: false),
                    TargetINR = table.Column<int>(nullable: false),
                    TargetINRAttach = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    CurrentDose = table.Column<int>(nullable: false),
                    INR = table.Column<int>(nullable: false),
                    NewDose = table.Column<int>(nullable: false),
                    NewINR = table.Column<int>(nullable: false),
                    BloodStatus = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: false),
                    PhysicianResponce = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remark = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientBloodCoagulationRecord", x => x.BloodRecordId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientBloodCoagulationRecord_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });
                migrationBuilder.CreateIndex(
                    name: "IX_tbl_ClientBloodCoagulationRecord_BloodRecordId",
                    table: "tbl_ClientBloodCoagulationRecord",
                    column: "BloodRecordId");
                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientBloodCoagulationRecord");
        }
    }
}
