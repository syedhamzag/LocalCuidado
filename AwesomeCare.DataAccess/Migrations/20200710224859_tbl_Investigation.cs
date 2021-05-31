using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_Investigation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Investigation",
                columns: table => new
                {
                    InvestigationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    IncidentClass = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(maxLength: 500, nullable: false),
                    IncidentDate = table.Column<DateTimeOffset>(nullable: false),
                    ConclusionDate = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Investigation", x => x.InvestigationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Investigation");
        }
    }
}
