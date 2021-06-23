using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffAdlObs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffAdlObs",
                columns: table => new
                {
                    ObservationID = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reference = table.Column<string>(maxLength: 50, nullable: false),
                    StaffId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    NextCheckDate = table.Column<DateTime>(nullable: false),
                    Details = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    UnderstandingofEquipment = table.Column<int>(nullable: false),
                    UnderstandingofService = table.Column<int>(nullable: false),
                    UnderstandingofControl = table.Column<int>(nullable: false),
                    FivePrinciples = table.Column<int>(nullable: false),
                    Comments = table.Column<string>(nullable: false),
                    ActionRequired = table.Column<string>(nullable: false),
                    //OfficertoAct = table.Column<int>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    URL = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffAdlObs", x => x.ObservationID);
                    table.ForeignKey(
                        name: "FK_tbl_StaffAdlObs_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffAdlObs_ObservationId",
                table: "tbl_StaffAdlObs",
                column: "ObservationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffAdlObs");
        }
    }
}