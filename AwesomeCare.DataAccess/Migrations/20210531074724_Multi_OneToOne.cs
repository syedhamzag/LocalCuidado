using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class Multi_OneToOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_OneToOne_OfficerToAct",
                columns: table => new
                {
                    OneToOneOfficerToActId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    OneToOneId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_OneToOne_OfficerToAct", x => x.OneToOneOfficerToActId);
                    table.ForeignKey(
                        name: "FK_tbl_OneToOne_OfficerToAct_tbl_ClientOneToOne_OneToOneId",
                        column: x => x.OneToOneId,
                        principalTable: "tbl_ClientOneToOne",
                        principalColumn: "OneToOneId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_OneToOne_OfficerToAct_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_tbl_OneToOne_OfficerToAct_OneToOneId",
                table: "tbl_OneToOne_OfficerToAct",
                column: "OneToOneId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_OneToOne_OfficerToAct");
        }
    }
}
