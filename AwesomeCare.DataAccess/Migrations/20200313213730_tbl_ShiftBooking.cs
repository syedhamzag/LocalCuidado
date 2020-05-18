using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ShiftBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ShiftBooking",
                columns: table => new
                {
                    ShiftBookingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShiftDate = table.Column<string>(maxLength: 15, nullable: false),
                    Rota = table.Column<int>(nullable: false),
                    NumberOfStaff = table.Column<int>(nullable: false),
                    StartTime = table.Column<string>(maxLength: 15, nullable: false),
                    StopTime = table.Column<string>(maxLength: 15, nullable: false),
                    Remark = table.Column<string>(maxLength: 225, nullable: false),
                    Team = table.Column<int>(nullable: false),
                    DriverRequired = table.Column<bool>(nullable: false),
                    PublishTo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ShiftBooking", x => x.ShiftBookingId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ShiftBooking");
        }
    }
}
