using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class StaffMedTracker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffMedTracker",
                columns: table => new
                {
                    StaffMedTrackerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedTrackDate = table.Column<DateTime>(nullable: false),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    RotaId = table.Column<int>(nullable: false),
                    ClientMedId = table.Column<int>(nullable: false),
                    DoseGiven = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffMedTracker", x => x.StaffMedTrackerId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffMedTracker");
        }
    }
}
