using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffEducation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffEducation",
                columns: table => new
                {
                    StaffEducationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    Institution = table.Column<string>(maxLength: 255, nullable: false),
                    Certificate = table.Column<string>(maxLength: 125, nullable: false),
                    Location = table.Column<string>(maxLength: 255, nullable: false),
                    Address = table.Column<string>(maxLength: 255, nullable: false),
                    StartDate = table.Column<string>(maxLength: 25, nullable: false),
                    EndDate = table.Column<string>(maxLength: 25, nullable: true),
                    CertificateAttachment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffEducation", x => x.StaffEducationId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffEducation_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffEducation_StaffPersonalInfoId",
                table: "tbl_StaffEducation",
                column: "StaffPersonalInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffEducation");
        }
    }
}
