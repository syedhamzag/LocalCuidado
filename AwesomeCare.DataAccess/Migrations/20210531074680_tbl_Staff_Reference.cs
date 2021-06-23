using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_Staff_Reference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Staff_Reference",
                columns: table => new
                {
                    StaffReferenceId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reference = table.Column<string>(maxLength: 50, nullable: false),
                    StaffId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    ApplicantRole = table.Column<int>(nullable: false),
                    DateOfEmployement = table.Column<int>(nullable: false),
                    DateOfExit = table.Column<string>(nullable: false),
                    RehireStaff = table.Column<string>(nullable: false),
                    Relationship = table.Column<string>(nullable: false),
                    TeamWork = table.Column<int>(nullable: false),
                    Integrity = table.Column<int>(nullable: false),
                    Knowledgeable = table.Column<int>(nullable: false),
                    WorkUnderPressure = table.Column<int>(nullable: false),
                    Caring = table.Column<int>(nullable: false),
                    PreviousExperience = table.Column<int>(nullable: false),
                    RefreeName = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Contact = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(nullable: false),
                    ConfirmedBy = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Staff_Reference", x => x.StaffReferenceId);
                    table.ForeignKey(
                        name: "FK_tbl_Staff_Reference_tbl_StaffPersonalInfo_StaffId",
                        column: x => x.StaffId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                }); ;
            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_Reference_StaffReferenceId",
                table: "tbl_Staff_Reference",
                column: "StaffReferenceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Staff_Reference");
        }
    }
}
