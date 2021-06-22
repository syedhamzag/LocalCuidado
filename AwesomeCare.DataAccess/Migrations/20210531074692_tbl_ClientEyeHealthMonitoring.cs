using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientEyeHealthMonitoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientEyeHealthMonitoring",
                columns: table => new
                {
                    EyeHealthId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reference = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    ToolUsed = table.Column<int>(nullable: false),
                    ToolUsedAttach = table.Column<string>(nullable: true),
                    MethodUsed = table.Column<int>(nullable: false),
                    MethodUsedAttach = table.Column<string>(nullable: true),
                    TargetSet = table.Column<int>(nullable: false),
                    CurrentScore = table.Column<int>(nullable: false),
                    PatientGlasses = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: false),
                    StatusImage = table.Column<int>(nullable: false),
                    PhysicianResponse = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    StatusAttach = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientEyeHealthMonitoring", x => x.EyeHealthId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientEyeHealthMonitoring_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);

                });
                migrationBuilder.CreateIndex(
                    name: "IX_tbl_ClientEyeHealthMonitoring_EyeHealthId",
                    table: "tbl_ClientEyeHealthMonitoring",
                    column: "EyeHealthId");
                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientEyeHealthMonitoring");
        }
    }
}
