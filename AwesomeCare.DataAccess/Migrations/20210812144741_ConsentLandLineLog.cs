using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class ConsentLandLineLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsentLandlineLog_tbl_ConsentLandLine_LandlineId",
                table: "ConsentLandlineLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConsentLandlineLog",
                table: "ConsentLandlineLog");

            migrationBuilder.RenameTable(
                name: "ConsentLandlineLog",
                newName: "tbl_ConsentLandlineLog");

            migrationBuilder.RenameIndex(
                name: "IX_ConsentLandlineLog_LandlineId",
                table: "tbl_ConsentLandlineLog",
                newName: "IX_tbl_ConsentLandlineLog_LandlineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_ConsentLandlineLog",
                table: "tbl_ConsentLandlineLog",
                column: "ConsentLandlineLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_ConsentLandlineLog_tbl_ConsentLandLine_LandlineId",
                table: "tbl_ConsentLandlineLog",
                column: "LandlineId",
                principalTable: "tbl_ConsentLandLine",
                principalColumn: "LandlineId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_ConsentLandlineLog_tbl_ConsentLandLine_LandlineId",
                table: "tbl_ConsentLandlineLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_ConsentLandlineLog",
                table: "tbl_ConsentLandlineLog");

            migrationBuilder.RenameTable(
                name: "tbl_ConsentLandlineLog",
                newName: "ConsentLandlineLog");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_ConsentLandlineLog_LandlineId",
                table: "ConsentLandlineLog",
                newName: "IX_ConsentLandlineLog_LandlineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConsentLandlineLog",
                table: "ConsentLandlineLog",
                column: "ConsentLandlineLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsentLandlineLog_tbl_ConsentLandLine_LandlineId",
                table: "ConsentLandlineLog",
                column: "LandlineId",
                principalTable: "tbl_ConsentLandLine",
                principalColumn: "LandlineId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
