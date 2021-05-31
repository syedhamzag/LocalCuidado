using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffReferee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffReferee",
                columns: table => new
                {
                    StaffRefereeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    Referee = table.Column<string>(maxLength: 125, nullable: false),
                    CompanyName = table.Column<string>(maxLength: 125, nullable: false),
                    Address = table.Column<string>(maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 125, nullable: false),
                    PositionofReferee = table.Column<string>(maxLength: 25, nullable: false),
                    Attachment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffReferee", x => x.StaffRefereeId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffReferee_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffReferee_StaffPersonalInfoId",
                table: "tbl_StaffReferee",
                column: "StaffPersonalInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffReferee");
        }
    }
}
