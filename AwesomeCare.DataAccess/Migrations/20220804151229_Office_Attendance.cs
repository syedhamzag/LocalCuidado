using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Office_Attendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_OfficeAttendance",
                columns: table => new
                {
                    AttendanceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    JobTitle = table.Column<string>(nullable: true),
                    Staff = table.Column<int>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    ClockInAddress = table.Column<string>(nullable: true),
                    ClockInDistance = table.Column<string>(nullable: true),
                    ClockOutAddress = table.Column<string>(nullable: true),
                    ClockOutDistance = table.Column<string>(nullable: true),
                    StartTime = table.Column<string>(nullable: true),
                    StopTime = table.Column<string>(nullable: true),
                    ClockIn = table.Column<DateTimeOffset>(nullable: true),
                    ClockOut = table.Column<DateTimeOffset>(nullable: true),
                    ClockInMethod = table.Column<string>(nullable: true),
                    ClockOutMethod = table.Column<string>(nullable: true),
                    ClockDiff = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    StaffPersonalInfoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_OfficeAttendance", x => x.AttendanceId);
                    table.ForeignKey(
                        name: "FK_tbl_OfficeAttendance_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_StaffOfficeLocation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Staff = table.Column<int>(nullable: false),
                    Location = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffOfficeLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_StaffOfficeLocation_tbl_OfficeLocation_Location",
                        column: x => x.Location,
                        principalTable: "tbl_OfficeLocation",
                        principalColumn: "OfficeLocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_StaffOfficeLocation_tbl_StaffPersonalInfo_Staff",
                        column: x => x.Staff,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OfficeAttendance_StaffPersonalInfoId",
                table: "tbl_OfficeAttendance",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffOfficeLocation_Location",
                table: "tbl_StaffOfficeLocation",
                column: "Location");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffOfficeLocation_Staff",
                table: "tbl_StaffOfficeLocation",
                column: "Staff");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_OfficeAttendance");

            migrationBuilder.DropTable(
                name: "tbl_StaffOfficeLocation");
        }
    }
}
