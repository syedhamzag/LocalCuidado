using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ShiftBooking_Team_StaffPersonalInfId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Team",
                table: "tbl_ShiftBooking",
                newName: "Team_StaffPersonalInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Team_StaffPersonalInfoId",
                table: "tbl_ShiftBooking",
                newName: "Team");
        }
    }
}
