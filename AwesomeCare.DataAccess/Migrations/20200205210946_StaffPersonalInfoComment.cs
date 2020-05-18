using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class StaffPersonalInfoComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffPersonalInfoComment",
                columns: table => new
                {
                    StaffPersonalInfoCommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    CommentBy_UserId = table.Column<int>(nullable: true),
                    Comment = table.Column<string>(maxLength: 250, nullable: false),
                    CommentOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffPersonalInfoComment", x => x.StaffPersonalInfoCommentId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffPersonalInfoComment_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffPersonalInfoComment_StaffPersonalInfoId",
                table: "tbl_StaffPersonalInfoComment",
                column: "StaffPersonalInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffPersonalInfoComment");
        }
    }
}
