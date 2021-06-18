using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientComplainRegister : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientComplainRegister",
                columns: table => new
                {
                    ComplainId = table.Column<int>(nullable:false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reference = table.Column<string>(maxLength: 50, nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    LINK = table.Column<string>(nullable: false),
                    IRFNUMBER = table.Column<string>(maxLength: 50, nullable: false),
                    INCIDENTDATE = table.Column<DateTime>(nullable: false),
                    DATERECIEVED = table.Column<DateTime>(nullable: false),
                    DATEOFACKNOWLEDGEMENT = table.Column<DateTime>(nullable: false),
                    OFFICERTOACTId = table.Column<int>(nullable: false),
                    SOURCEOFCOMPLAINTS = table.Column<string>(nullable: false),
                    COMPLAINANTCONTACT = table.Column<string>(maxLength: 50, nullable: false),
                    STAFFId = table.Column<int>(nullable: false),
                    CONCERNSRAISED = table.Column<string>(nullable: false),
                    DUEDATE = table.Column<DateTime>(nullable: false),
                    LETTERTOSTAFF = table.Column<string>(nullable: false),
                    INVESTIGATIONOUTCOME = table.Column<string>(nullable: false),
                    ACTIONTAKEN = table.Column<string>(maxLength: 50, nullable: false),
                    FINALRESPONSETOFAMILY = table.Column<string>(nullable: false),
                    ROOTCAUSE = table.Column<string>(maxLength: 50, nullable: false),
                    REMARK = table.Column<string>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    EvidenceFilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientComplainRegister", x => x.ComplainId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientComplainRegister_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });
                migrationBuilder.CreateIndex(
                    name: "IX_tbl_ClientComplainRegister_ComplainId",
                    table: "tbl_ClientComplainRegister",
                    column: "ComplainId");
                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientComplainRegister");
        }
    }
}
