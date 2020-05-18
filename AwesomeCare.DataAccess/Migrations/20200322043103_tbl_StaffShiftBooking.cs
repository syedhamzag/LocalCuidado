using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffShiftBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffShiftBooking",
                columns: table => new
                {
                    StaffShiftBookingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RotaId = table.Column<int>(nullable: false),
                    MonthIndex = table.Column<int>(nullable: false),
                    MonthName = table.Column<string>(maxLength: 25, nullable: false),
                    Year = table.Column<int>(nullable: false),
                    StaffPersonalInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffShiftBooking", x => x.StaffShiftBookingId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffShiftBooking_tbl_ClientRotaName_RotaId",
                        column: x => x.RotaId,
                        principalTable: "tbl_ClientRotaName",
                        principalColumn: "RotaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_StaffShiftBooking_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffShiftBooking_RotaId",
                table: "tbl_StaffShiftBooking",
                column: "RotaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffShiftBooking_StaffPersonalInfoId",
                table: "tbl_StaffShiftBooking",
                column: "StaffPersonalInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffShiftBooking");
        }
    }
}
