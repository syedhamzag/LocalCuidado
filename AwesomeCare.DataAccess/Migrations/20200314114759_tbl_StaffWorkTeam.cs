using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffWorkTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "Team_StaffPersonalInfId",
            //    table: "tbl_ShiftBooking",
            //    newName: "Team_StaffPersonalInfoId");

            migrationBuilder.AddColumn<int>(
                name: "StaffWorkTeamId",
                table: "tbl_StaffPersonalInfo",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tbl_StaffWorkTeam",
                columns: table => new
                {
                    StaffWorkTeamId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WorkTeam = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffWorkTeam", x => x.StaffWorkTeamId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffPersonalInfo_StaffWorkTeamId",
                table: "tbl_StaffPersonalInfo",
                column: "StaffWorkTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_StaffPersonalInfo_tbl_StaffWorkTeam_StaffWorkTeamId",
                table: "tbl_StaffPersonalInfo",
                column: "StaffWorkTeamId",
                principalTable: "tbl_StaffWorkTeam",
                principalColumn: "StaffWorkTeamId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_StaffPersonalInfo_tbl_StaffWorkTeam_StaffWorkTeamId",
                table: "tbl_StaffPersonalInfo");

            migrationBuilder.DropTable(
                name: "tbl_StaffWorkTeam");

            migrationBuilder.DropIndex(
                name: "IX_tbl_StaffPersonalInfo_StaffWorkTeamId",
                table: "tbl_StaffPersonalInfo");

            migrationBuilder.DropColumn(
                name: "StaffWorkTeamId",
                table: "tbl_StaffPersonalInfo");

            //migrationBuilder.RenameColumn(
            //    name: "Team_StaffPersonalInfoId",
            //    table: "tbl_ShiftBooking",
            //    newName: "Team_StaffPersonalInfId");
        }
    }
}
