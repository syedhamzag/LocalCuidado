using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class AddOneToOneCompany_CompanyContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "tbl_CompanyContact",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_CompanyContact_tbl_Company_CompanyId",
                table: "tbl_CompanyContact",
                column: "CompanyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_CompanyContact_tbl_Company_CompanyId",
                table: "tbl_CompanyContact",
                column: "CompanyId",
                principalTable: "tbl_Company",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_CompanyContact_tbl_Company_CompanyId",
                table: "tbl_CompanyContact");

            migrationBuilder.DropIndex(
                name: "IX_tbl_CompanyContact_tbl_Company_CompanyId",
                table: "tbl_CompanyContact");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "tbl_CompanyContact");
        }
    }
}
