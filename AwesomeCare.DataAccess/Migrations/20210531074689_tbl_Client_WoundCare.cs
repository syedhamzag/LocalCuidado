using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_Client_WoundCare : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Client_WoundCare",
                columns: table => new
                {
                    WoundCareId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reference = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Goal = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    TypeAttach = table.Column<string>(nullable: true),
                    UlcerStage = table.Column<int>(nullable: false),
                    UlcerStageAttach = table.Column<string>(nullable: true),
                    Measurment = table.Column<int>(nullable: false),
                    MeasurementAttach = table.Column<string>(nullable: true),
                    PainLvl = table.Column<int>(nullable: false),
                    Location = table.Column<int>(nullable: false),
                    LocationAttach = table.Column<string>(nullable: true),
                    WoundCause = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: false),
                    StatusImage = table.Column<int>(nullable: false),
                    StatusAttach = table.Column<string>(nullable: true),
                    PhysicianResponse = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Client_WoundCare", x => x.WoundCareId);
                    table.ForeignKey(
                        name: "FK_tbl_Client_WoundCare_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });
                migrationBuilder.CreateIndex(
                    name: "IX_tbl_Client_WoundCare_WoundCareId",
                    table: "tbl_Client_WoundCare",
                    column: "WoundCareId");
                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Client_WoundCare");
        }
    }
}
