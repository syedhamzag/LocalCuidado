using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffSpotCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffSpotCheck",
                columns: table => new
                {
                    SpotCheckId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reference = table.Column<string>(maxLength: 50, nullable: false),
                    StaffId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NextCheckDate = table.Column<DateTime>(nullable: false),
                    Details = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    StaffArriveOnTime = table.Column<int>(nullable: false),
                    StaffDressCode = table.Column<int>(nullable: false),
                    AreaComments = table.Column<string>(nullable: false),
                    ActionRequired = table.Column<string>(nullable: false),
                    //OfficerToAct = table.Column<int>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    URL = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffSpotCheck", x => x.SpotCheckId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffSpotCheck_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffSpotCheck_SpotCheckId",
                table: "tbl_StaffSpotCheck",
                column: "SpotCheckId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffSpotCheck");
        }
    }
}