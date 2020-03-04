using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffCommunication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_StaffCommunication",
                columns: table => new
                {
                    StaffCommunicationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ValueDate = table.Column<DateTime>(nullable: false),
                    Concern = table.Column<string>(maxLength: 500, nullable: false),
                    CommunicationClass = table.Column<int>(nullable: false),
                    PersonInvolved = table.Column<int>(nullable: false),
                    ExpectedAction = table.Column<string>(maxLength: 255, nullable: false),
                    ActionTaken = table.Column<string>(maxLength: 255, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Telephone = table.Column<string>(maxLength: 50, nullable: false),
                    PersonResponsibleForAction = table.Column<int>(nullable: false),
                    Attachment = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffCommunication", x => x.StaffCommunicationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_StaffCommunication");
        }
    }
}
