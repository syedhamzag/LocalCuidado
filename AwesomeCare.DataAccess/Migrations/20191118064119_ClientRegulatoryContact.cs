using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class ClientRegulatoryContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                table: "tbl_Client",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.CreateTable(
                name: "tbl_ClientRegulatoryContact",
                columns: table => new
                {
                    ClientRegulatoryContactId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<int>(nullable: false),
                    BaseRecordItemId = table.Column<int>(nullable: false),
                    DatePerformed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Evidence = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientRegulatoryContact", x => x.ClientRegulatoryContactId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientRegulatoryContact_tbl_BaseRecordItem_BaseRecordItemId",
                        column: x => x.BaseRecordItemId,
                        principalTable: "tbl_BaseRecordItem",
                        principalColumn: "BaseRecordItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ClientRegulatoryContact_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientRegulatoryContact_BaseRecordItemId",
                table: "tbl_ClientRegulatoryContact",
                column: "BaseRecordItemId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientRegulatoryContact_ClientId",
                table: "tbl_ClientRegulatoryContact",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientRegulatoryContact");

            migrationBuilder.AlterColumn<decimal>(
                name: "Rate",
                table: "tbl_Client",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
