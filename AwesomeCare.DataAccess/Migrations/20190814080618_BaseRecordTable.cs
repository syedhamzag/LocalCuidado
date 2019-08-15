using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class BaseRecordTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_BaseRecord",
                columns: table => new
                {
                    BaseRecordId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KeyName = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BaseRecord", x => x.BaseRecordId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BaseRecordItem",
                columns: table => new
                {
                    BaseRecordItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BaseRecordId = table.Column<int>(nullable: false),
                    ValueName = table.Column<string>(maxLength: 225, nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BaseRecordItem", x => x.BaseRecordItemId);
                    table.ForeignKey(
                        name: "FK_tbl_BaseRecordItem_tbl_BaseRecord_BaseRecordId",
                        column: x => x.BaseRecordId,
                        principalTable: "tbl_BaseRecord",
                        principalColumn: "BaseRecordId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BaseRecordItem_BaseRecordId",
                table: "tbl_BaseRecordItem",
                column: "BaseRecordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_BaseRecordItem");

            migrationBuilder.DropTable(
                name: "tbl_BaseRecord");
        }
    }
}
