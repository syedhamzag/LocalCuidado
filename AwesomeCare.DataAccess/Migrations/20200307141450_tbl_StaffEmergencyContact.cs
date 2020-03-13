using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffEmergencyContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffEmergencyContact",
                columns: table => new
                {
                    StaffEmergencyContactId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    ContactName = table.Column<string>(maxLength: 100, nullable: false),
                    Telephone = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    Relationship = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffEmergencyContact", x => x.StaffEmergencyContactId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffEmergencyContact_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UntowardsOfficerToAct_StaffPersonalInfoId",
                table: "tbl_UntowardsOfficerToAct",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffEmergencyContact_StaffPersonalInfoId",
                table: "tbl_StaffEmergencyContact",
                column: "StaffPersonalInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_UntowardsOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "tbl_UntowardsOfficerToAct",
                column: "StaffPersonalInfoId",
                principalTable: "tbl_StaffPersonalInfo",
                principalColumn: "StaffPersonalInfoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_UntowardsOfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "tbl_UntowardsOfficerToAct");

            migrationBuilder.DropTable(
                name: "tbl_StaffEmergencyContact");

            migrationBuilder.DropIndex(
                name: "IX_tbl_UntowardsOfficerToAct_StaffPersonalInfoId",
                table: "tbl_UntowardsOfficerToAct");
        }
    }
}
