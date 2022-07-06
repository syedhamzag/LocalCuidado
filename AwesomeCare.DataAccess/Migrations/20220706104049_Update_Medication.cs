using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Update_Medication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RotaId",
                table: "tbl_ClientMedicationPeriod",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StartTime",
                table: "tbl_ClientMedicationPeriod",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StopTime",
                table: "tbl_ClientMedicationPeriod",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tbl_StaffMedRota",
                columns: table => new
                {
                    StaffRotaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RotaDate = table.Column<DateTime>(type: "date", nullable: false),
                    RotaDayofWeekId = table.Column<int>(nullable: true),
                    Staff = table.Column<int>(nullable: false),
                    RotaId = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(maxLength: 225, nullable: true),
                    ReferenceNumber = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffMedRota", x => x.StaffRotaId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffMedRota_tbl_ClientRotaName_RotaId",
                        column: x => x.RotaId,
                        principalTable: "tbl_ClientRotaName",
                        principalColumn: "RotaId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffMedRota_RotaId",
                table: "tbl_StaffMedRota",
                column: "RotaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffMedRota");

            migrationBuilder.DropColumn(
                name: "RotaId",
                table: "tbl_ClientMedicationPeriod");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "tbl_ClientMedicationPeriod");

            migrationBuilder.DropColumn(
                name: "StopTime",
                table: "tbl_ClientMedicationPeriod");
        }
    }
}
