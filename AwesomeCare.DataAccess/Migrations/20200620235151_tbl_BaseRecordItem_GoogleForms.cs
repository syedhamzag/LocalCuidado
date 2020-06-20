using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_BaseRecordItem_GoogleForms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddLink",
                table: "tbl_BaseRecordItem",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasGoogleForm",
                table: "tbl_BaseRecordItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ViewLink",
                table: "tbl_BaseRecordItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddLink",
                table: "tbl_BaseRecordItem");

            migrationBuilder.DropColumn(
                name: "HasGoogleForm",
                table: "tbl_BaseRecordItem");

            migrationBuilder.DropColumn(
                name: "ViewLink",
                table: "tbl_BaseRecordItem");
        }
    }
}
