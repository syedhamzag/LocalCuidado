using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_RotaTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_RotaTask",
                columns: table => new
                {
                    Deleted = table.Column<bool>(nullable: false),
                    RotaTaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TaskName = table.Column<string>(maxLength: 125, nullable: false),
                    GivenAcronym = table.Column<string>(maxLength: 50, nullable: false),
                    NotGivenAcronym = table.Column<string>(maxLength: 50, nullable: false),
                    Remark = table.Column<string>(maxLength: 125, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_RotaTask", x => x.RotaTaskId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_RotaTask_GivenAcronym",
                table: "tbl_RotaTask",
                column: "GivenAcronym",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_RotaTask_NotGivenAcronym",
                table: "tbl_RotaTask",
                column: "NotGivenAcronym",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_RotaTask_TaskName",
                table: "tbl_RotaTask",
                column: "TaskName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_RotaTask");
        }
    }
}
