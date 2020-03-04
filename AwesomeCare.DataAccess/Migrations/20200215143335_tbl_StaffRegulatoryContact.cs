using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffRegulatoryContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffRegulatoryContact",
                columns: table => new
                {
                    StaffRegulatoryContactId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    BaseRecordItemId = table.Column<int>(nullable: false),
                    DatePerformed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Evidence = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffRegulatoryContact", x => x.StaffRegulatoryContactId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffRegulatoryContact_tbl_BaseRecordItem_BaseRecordItemId",
                        column: x => x.BaseRecordItemId,
                        principalTable: "tbl_BaseRecordItem",
                        principalColumn: "BaseRecordItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_StaffRegulatoryContact_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffRegulatoryContact_BaseRecordItemId",
                table: "tbl_StaffRegulatoryContact",
                column: "BaseRecordItemId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffRegulatoryContact_StaffPersonalInfoId",
                table: "tbl_StaffRegulatoryContact",
                column: "StaffPersonalInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffRegulatoryContact");
        }
    }
}
