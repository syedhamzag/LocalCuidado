using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class StaffHoliday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffHoliday",
                columns: table => new
                {
                    StaffHolidayId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    YearOfService = table.Column<decimal>(nullable: false),
                    AllocatedDays = table.Column<decimal>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Days = table.Column<decimal>(nullable: false),
                    Purpose = table.Column<string>(nullable: false),
                    Class = table.Column<int>(nullable: false),
                    PersonOnResponsibility = table.Column<string>(nullable: false),
                    CopyOfHandover = table.Column<string>(nullable: false),
                    Remark = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffHoliday", x => x.StaffHolidayId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffHoliday_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffHoliday_StaffPersonalInfoId",
                table: "tbl_StaffHoliday",
                column: "StaffPersonalInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffHoliday");
        }
    }
}
