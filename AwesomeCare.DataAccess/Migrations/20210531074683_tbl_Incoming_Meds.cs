using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_Incoming_Meds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Incoming_Meds",
                columns: table => new
                {
                    IncomingMedsId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<int>(nullable: false),
                    StaffName = table.Column<string>(nullable: false),
                    StartDate = table.Column<string>(nullable: false),
                    ChartImage = table.Column<string>(nullable: true),
                    MedsImage = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Incoming_MedsId", x => x.IncomingMedsId);
                    table.ForeignKey(
                        name: "FK_tbl_Incoming_Meds_tbl_Client_UserName",
                        column: x => x.UserName,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                }); ;
            migrationBuilder.CreateIndex(
                name: "IX_tbl_Incoming_Meds_IncomingMedsId",
                table: "tbl_Incoming_Meds",
                column: "IncomingMedsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Incoming_Meds");
        }
    }
}
