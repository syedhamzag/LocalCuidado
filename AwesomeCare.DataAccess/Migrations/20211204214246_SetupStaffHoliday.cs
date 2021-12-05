using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class SetupStaffHoliday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_SetupStaffHoliday",
                columns: table => new
                {
                    SetupHolidayId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    YearOfEmployment = table.Column<DateTime>(nullable: false),
                    TypeOfHoliday = table.Column<int>(nullable: false),
                    YearEndPeriodStartDate = table.Column<DateTime>(nullable: false),
                    HoursSoFar = table.Column<int>(nullable: false),
                    IncrementalDailyHolidayByHrs = table.Column<int>(nullable: false),
                    NumberOfDays = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SetupStaffHoliday", x => x.SetupHolidayId);
                    table.ForeignKey(
                        name: "FK_tbl_SetupStaffHoliday_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_SetupStaffHoliday_StaffPersonalInfoId",
                table: "tbl_SetupStaffHoliday",
                column: "StaffPersonalInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_SetupStaffHoliday");
        }
    }
}
