using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffTraining : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffTraining",
                columns: table => new
                {
                    StaffTrainingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    Training = table.Column<string>(maxLength: 255, nullable: false),
                    Certificate = table.Column<string>(maxLength: 125, nullable: false),
                    Location = table.Column<string>(maxLength: 255, nullable: false),
                    Trainer = table.Column<string>(maxLength: 125, nullable: false),
                    StartDate = table.Column<string>(maxLength: 25, nullable: false),
                    ExpiredDate = table.Column<string>(maxLength: 25, nullable: true),
                    CertificateAttachment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffTraining", x => x.StaffTrainingId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffTraining_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffTraining_StaffPersonalInfoId",
                table: "tbl_StaffTraining",
                column: "StaffPersonalInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffTraining");
        }
    }
}
