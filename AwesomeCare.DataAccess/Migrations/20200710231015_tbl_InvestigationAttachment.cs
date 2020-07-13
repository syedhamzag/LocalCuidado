using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_InvestigationAttachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_InvestigationAttachment",
                columns: table => new
                {
                    InvestigationAttachmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvestigationId = table.Column<int>(nullable: false),
                    Attachment = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_InvestigationAttachment", x => x.InvestigationAttachmentId);
                    table.ForeignKey(
                        name: "FK_tbl_InvestigationAttachment_tbl_Investigation_InvestigationId",
                        column: x => x.InvestigationId,
                        principalTable: "tbl_Investigation",
                        principalColumn: "InvestigationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_InvestigationAttachment_InvestigationId",
                table: "tbl_InvestigationAttachment",
                column: "InvestigationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_InvestigationAttachment");
        }
    }
}
