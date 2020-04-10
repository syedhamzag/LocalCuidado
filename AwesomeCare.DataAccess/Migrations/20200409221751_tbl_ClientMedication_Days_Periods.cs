using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientMedication_Days_Periods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientMedication",
                columns: table => new
                {
                    ClientMedicationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationId = table.Column<int>(nullable: false),
                    MedicationManufacturerId = table.Column<int>(nullable: false),
                    ExpiryDate = table.Column<string>(maxLength: 15, nullable: false),
                    Dossage = table.Column<string>(maxLength: 50, nullable: false),
                    Frequency = table.Column<string>(maxLength: 50, nullable: false),
                    Gap_Hour = table.Column<int>(nullable: false),
                    Route = table.Column<string>(maxLength: 50, nullable: false),
                    StartDate = table.Column<string>(maxLength: 15, nullable: false),
                    StopDate = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: false),
                    Remark = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientMedication", x => x.ClientMedicationId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientMedication_tbl_Medication_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "tbl_Medication",
                        principalColumn: "MedicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ClientMedication_tbl_MedicationManufacturer_MedicationManufacturerId",
                        column: x => x.MedicationManufacturerId,
                        principalTable: "tbl_MedicationManufacturer",
                        principalColumn: "MedicationManufacturerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ClientMedicationDay",
                columns: table => new
                {
                    ClientMedicationDayId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientMedicationId = table.Column<int>(nullable: false),
                    RotaDayofWeekId = table.Column<int>(nullable: false),
                    ClientMedicationId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientMedicationDay", x => x.ClientMedicationDayId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientMedicationDay_tbl_ClientMedication_ClientMedicationId",
                        column: x => x.ClientMedicationId,
                        principalTable: "tbl_ClientMedication",
                        principalColumn: "ClientMedicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ClientMedicationDay_tbl_ClientMedication_ClientMedicationId1",
                        column: x => x.ClientMedicationId1,
                        principalTable: "tbl_ClientMedication",
                        principalColumn: "ClientMedicationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_ClientMedicationDay_tbl_RotaDayofWeek_RotaDayofWeekId",
                        column: x => x.RotaDayofWeekId,
                        principalTable: "tbl_RotaDayofWeek",
                        principalColumn: "RotaDayofWeekId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ClientMedicationPeriod",
                columns: table => new
                {
                    ClientMedicationPeriodId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientRotaTypeId = table.Column<int>(nullable: false),
                    ClientMedicationDayId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientMedicationPeriod", x => x.ClientMedicationPeriodId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientMedicationPeriod_tbl_ClientMedicationDay_ClientMedicationDayId",
                        column: x => x.ClientMedicationDayId,
                        principalTable: "tbl_ClientMedicationDay",
                        principalColumn: "ClientMedicationDayId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ClientMedicationPeriod_tbl_ClientRotaType_ClientRotaTypeId",
                        column: x => x.ClientRotaTypeId,
                        principalTable: "tbl_ClientRotaType",
                        principalColumn: "ClientRotaTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientMedication_MedicationId",
                table: "tbl_ClientMedication",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientMedication_MedicationManufacturerId",
                table: "tbl_ClientMedication",
                column: "MedicationManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientMedicationDay_ClientMedicationId",
                table: "tbl_ClientMedicationDay",
                column: "ClientMedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientMedicationDay_ClientMedicationId1",
                table: "tbl_ClientMedicationDay",
                column: "ClientMedicationId1");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientMedicationDay_RotaDayofWeekId",
                table: "tbl_ClientMedicationDay",
                column: "RotaDayofWeekId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientMedicationPeriod_ClientMedicationDayId",
                table: "tbl_ClientMedicationPeriod",
                column: "ClientMedicationDayId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientMedicationPeriod_ClientRotaTypeId",
                table: "tbl_ClientMedicationPeriod",
                column: "ClientRotaTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientMedicationPeriod");

            migrationBuilder.DropTable(
                name: "tbl_ClientMedicationDay");

            migrationBuilder.DropTable(
                name: "tbl_ClientMedication");
        }
    }
}
