using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffRotaTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffRotaTask",
                columns: table => new
                {
                    StaffRotaTaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffRotaPeriodId = table.Column<int>(nullable: false),
                    RotaTaskId = table.Column<int>(nullable: false),
                    IsGiven = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffRotaTask", x => x.StaffRotaTaskId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffRotaTask_tbl_StaffRotaPeriod_StaffRotaPeriodId",
                        column: x => x.StaffRotaPeriodId,
                        principalTable: "tbl_StaffRotaPeriod",
                        principalColumn: "StaffRotaPeriodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffRotaTask_StaffRotaPeriodId",
                table: "tbl_StaffRotaTask",
                column: "StaffRotaPeriodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffRotaTask");
        }
    }
}
