using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_ClientSeizure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ClientSeizure",
                columns: table => new
                {
                    SeizureId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Reference = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    SeizureType = table.Column<int>(nullable: false),
                    SeizureTypeAttach = table.Column<string>(nullable: true),
                    SeizureLength = table.Column<int>(nullable: false),
                    SeizureLengthAttach = table.Column<string>(nullable: true),
                    Often = table.Column<int>(nullable: false),
                    WhatHappened = table.Column<string>(nullable: false),
                    StatusImage = table.Column<int>(nullable: false),
                    StatusAttach = table.Column<string>(nullable: true),
                    PhysicianResponse = table.Column<string>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)     
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ClientSeizure", x => x.SeizureId);
                    table.ForeignKey(
                        name: "FK_tbl_ClientSeizure_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);

                });
                migrationBuilder.CreateIndex(
                    name: "IX_tbl_ClientSeizure_SeizureId",
                    table: "tbl_ClientSeizure",
                    column: "SeizureId");
                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ClientSeizure");
        }
    }
}
