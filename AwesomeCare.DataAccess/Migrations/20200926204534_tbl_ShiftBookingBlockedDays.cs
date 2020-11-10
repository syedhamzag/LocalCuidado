using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ShiftBookingBlockedDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ShiftBookingBlockedDays",
                columns: table => new
                {
                    ShiftBookingBlockedDaysId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShiftBookingId = table.Column<int>(nullable: false),
                    Day = table.Column<string>(maxLength: 2, nullable: false),
                    WeekDay = table.Column<string>(maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ShiftBookingBlockedDays", x => x.ShiftBookingBlockedDaysId);
                    table.ForeignKey(
                        name: "FK_tbl_ShiftBookingBlockedDays_tbl_ShiftBooking_ShiftBookingId",
                        column: x => x.ShiftBookingId,
                        principalTable: "tbl_ShiftBooking",
                        principalColumn: "ShiftBookingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ShiftBookingBlockedDays_ShiftBookingId",
                table: "tbl_ShiftBookingBlockedDays",
                column: "ShiftBookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ShiftBookingBlockedDays");
        }
    }
}
