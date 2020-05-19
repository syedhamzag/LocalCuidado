using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffRating_tbl_staffpersonalinfoUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EmploymentDate",
                table: "tbl_StaffPersonalInfo",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasIdCard",
                table: "tbl_StaffPersonalInfo",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasUniform",
                table: "tbl_StaffPersonalInfo",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTeamLeader",
                table: "tbl_StaffPersonalInfo",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobCategory",
                table: "tbl_StaffPersonalInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceOfBirth",
                table: "tbl_StaffPersonalInfo",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tbl_StaffRating",
                columns: table => new
                {
                    StaffRatingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: false),
                    CommentDate = table.Column<DateTime>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    RatedBy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffRating", x => x.StaffRatingId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffRating_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffRating_StaffPersonalInfoId",
                table: "tbl_StaffRating",
                column: "StaffPersonalInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffRating");

            migrationBuilder.DropColumn(
                name: "EmploymentDate",
                table: "tbl_StaffPersonalInfo");

            migrationBuilder.DropColumn(
                name: "HasIdCard",
                table: "tbl_StaffPersonalInfo");

            migrationBuilder.DropColumn(
                name: "HasUniform",
                table: "tbl_StaffPersonalInfo");

            migrationBuilder.DropColumn(
                name: "IsTeamLeader",
                table: "tbl_StaffPersonalInfo");

            migrationBuilder.DropColumn(
                name: "JobCategory",
                table: "tbl_StaffPersonalInfo");

            migrationBuilder.DropColumn(
                name: "PlaceOfBirth",
                table: "tbl_StaffPersonalInfo");
        }
    }
}
