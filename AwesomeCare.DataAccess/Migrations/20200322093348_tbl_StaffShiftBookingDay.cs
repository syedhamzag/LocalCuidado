using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffShiftBookingDay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffShiftBookingDay",
                columns: table => new
                {
                    StaffShiftBookingDayId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffShiftBookingId = table.Column<int>(nullable: false),
                    Day = table.Column<string>(maxLength: 2, nullable: false),
                    WeekDay = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffShiftBookingDay", x => x.StaffShiftBookingDayId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffShiftBookingDay_tbl_StaffShiftBooking_StaffShiftBookingId",
                        column: x => x.StaffShiftBookingId,
                        principalTable: "tbl_StaffShiftBooking",
                        principalColumn: "StaffShiftBookingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffShiftBookingDay_StaffShiftBookingId",
                table: "tbl_StaffShiftBookingDay",
                column: "StaffShiftBookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffShiftBookingDay");
        }
    }
}
