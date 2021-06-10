using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffReference",
                columns: table => new
                {
                    StaffReferenceId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    ApplicantRole = table.Column<int>(nullable: false),
                    DateOfEmployement = table.Column<int>(nullable: false),
                    DateOfExit = table.Column<string>(maxLength: 255, nullable: false),
                    RehireStaff = table.Column<string>(maxLength: 255, nullable: false),
                    Relationship = table.Column<string>(maxLength: 255, nullable: false),
                    TeamWork = table.Column<int>(nullable: false),
                    Integrity = table.Column<int>(nullable: false),
                    Knowledgeable = table.Column<int>(nullable: false),
                    WorkUnderPressure = table.Column<int>(nullable: false),
                    Caring = table.Column<int>(nullable: false),
                    PreviousExperience = table.Column<int>(nullable: false),
                    RefreeName = table.Column<string>(maxLength: 255, nullable: false),
                    Address = table.Column<string>(maxLength: 255, nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    Contact = table.Column<string>(maxLength: 255, nullable: false),
                    Attach = table.Column<string>(maxLength: 255, nullable: false),
                    ConfirmedBy = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffReference", x => x.StaffReferenceId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffReference_tbl_StaffPersonalInfo_StaffId",
                        column: x => x.StaffId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                }); ;
            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffReference_StaffReferenceId",
                table: "tbl_StaffReference",
                column: "StaffReferenceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffReference");
        }
    }
}
