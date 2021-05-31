using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffShiftBooking_ColumnChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from tbl_staffshiftbooking");
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_StaffShiftBooking_tbl_ClientRotaName_RotaId",
                table: "tbl_StaffShiftBooking");

            migrationBuilder.DropColumn(
                name: "MonthIndex",
                table: "tbl_StaffShiftBooking");

            migrationBuilder.DropColumn(
                name: "MonthName",
                table: "tbl_StaffShiftBooking");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "tbl_StaffShiftBooking",
                newName: "ShiftBookingId");

            migrationBuilder.AlterColumn<int>(
                name: "RotaId",
                table: "tbl_StaffShiftBooking",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffShiftBooking_ShiftBookingId",
                table: "tbl_StaffShiftBooking",
                column: "ShiftBookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_StaffShiftBooking_tbl_ClientRotaName_RotaId",
                table: "tbl_StaffShiftBooking",
                column: "RotaId",
                principalTable: "tbl_ClientRotaName",
                principalColumn: "RotaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_StaffShiftBooking_tbl_ShiftBooking_ShiftBookingId",
                table: "tbl_StaffShiftBooking",
                column: "ShiftBookingId",
                principalTable: "tbl_ShiftBooking",
                principalColumn: "ShiftBookingId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_StaffShiftBooking_tbl_ClientRotaName_RotaId",
                table: "tbl_StaffShiftBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_StaffShiftBooking_tbl_ShiftBooking_ShiftBookingId",
                table: "tbl_StaffShiftBooking");

            migrationBuilder.DropIndex(
                name: "IX_tbl_StaffShiftBooking_ShiftBookingId",
                table: "tbl_StaffShiftBooking");

            migrationBuilder.RenameColumn(
                name: "ShiftBookingId",
                table: "tbl_StaffShiftBooking",
                newName: "Year");

            migrationBuilder.AlterColumn<int>(
                name: "RotaId",
                table: "tbl_StaffShiftBooking",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MonthIndex",
                table: "tbl_StaffShiftBooking",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MonthName",
                table: "tbl_StaffShiftBooking",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_StaffShiftBooking_tbl_ClientRotaName_RotaId",
                table: "tbl_StaffShiftBooking",
                column: "RotaId",
                principalTable: "tbl_ClientRotaName",
                principalColumn: "RotaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
