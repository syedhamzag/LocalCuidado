using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class FilesAndRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ValueName",
                table: "tbl_BaseRecordItem",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(225)",
                oldMaxLength: 225);

            migrationBuilder.AddColumn<int>(
                name: "ExpiryInMonths",
                table: "tbl_BaseRecordItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "tbl_BestInterestAssessment",
                columns: table => new
                {
                    BestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Position = table.Column<string>(nullable: false),
                    Signature = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BestInterestAssessment", x => x.BestId);
                    table.ForeignKey(
                        name: "FK_tbl_BestInterestAssessment_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_FilesAndRecord",
                columns: table => new
                {
                    FilesAndRecordId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    Subject = table.Column<string>(nullable: false),
                    StaffPersonalInfoId = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: false),
                    Attachment = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_FilesAndRecord", x => x.FilesAndRecordId);
                    table.ForeignKey(
                        name: "FK_tbl_FilesAndRecord_tbl_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "tbl_Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_FilesAndRecord_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_StaffTrainingMatrix",
                columns: table => new
                {
                    MatrixId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffPersonalInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffTrainingMatrix", x => x.MatrixId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffTrainingMatrix_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                        column: x => x.StaffPersonalInfoId,
                        principalTable: "tbl_StaffPersonalInfo",
                        principalColumn: "StaffPersonalInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BelieveTask",
                columns: table => new
                {
                    BelieveTaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BestId = table.Column<int>(nullable: false),
                    ReasonableBelieve = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BelieveTask", x => x.BelieveTaskId);
                    table.ForeignKey(
                        name: "FK_tbl_BelieveTask_tbl_BestInterestAssessment_BestId",
                        column: x => x.BestId,
                        principalTable: "tbl_BestInterestAssessment",
                        principalColumn: "BestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_CareIssuesTask",
                columns: table => new
                {
                    CareIssuesTaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BestId = table.Column<int>(nullable: false),
                    Issues = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_CareIssuesTask", x => x.CareIssuesTaskId);
                    table.ForeignKey(
                        name: "FK_tbl_CareIssuesTask_tbl_BestInterestAssessment_BestId",
                        column: x => x.BestId,
                        principalTable: "tbl_BestInterestAssessment",
                        principalColumn: "BestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_HealthTask",
                columns: table => new
                {
                    HealthTaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BestId = table.Column<int>(nullable: false),
                    HeadingId = table.Column<int>(nullable: false),
                    Title = table.Column<int>(nullable: false),
                    Answer = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_HealthTask", x => x.HealthTaskId);
                    table.ForeignKey(
                        name: "FK_tbl_HealthTask_tbl_BestInterestAssessment_BestId",
                        column: x => x.BestId,
                        principalTable: "tbl_BestInterestAssessment",
                        principalColumn: "BestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_HealthTask2",
                columns: table => new
                {
                    HealthTask2Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BestId = table.Column<int>(nullable: false),
                    Heading2Id = table.Column<int>(nullable: false),
                    Title = table.Column<int>(nullable: false),
                    Answer = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_HealthTask2", x => x.HealthTask2Id);
                    table.ForeignKey(
                        name: "FK_tbl_HealthTask2_tbl_BestInterestAssessment_BestId",
                        column: x => x.BestId,
                        principalTable: "tbl_BestInterestAssessment",
                        principalColumn: "BestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_StaffTrainingMatrixList",
                columns: table => new
                {
                    TrainingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatrixId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_StaffTrainingMatrixList", x => x.TrainingId);
                    table.ForeignKey(
                        name: "FK_tbl_StaffTrainingMatrixList_tbl_StaffTrainingMatrix_MatrixId",
                        column: x => x.MatrixId,
                        principalTable: "tbl_StaffTrainingMatrix",
                        principalColumn: "MatrixId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BelieveTask_BestId",
                table: "tbl_BelieveTask",
                column: "BestId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BestInterestAssessment_ClientId",
                table: "tbl_BestInterestAssessment",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_CareIssuesTask_BestId",
                table: "tbl_CareIssuesTask",
                column: "BestId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_FilesAndRecord_ClientId",
                table: "tbl_FilesAndRecord",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_FilesAndRecord_StaffPersonalInfoId",
                table: "tbl_FilesAndRecord",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HealthTask_BestId",
                table: "tbl_HealthTask",
                column: "BestId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_HealthTask2_BestId",
                table: "tbl_HealthTask2",
                column: "BestId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffTrainingMatrix_StaffPersonalInfoId",
                table: "tbl_StaffTrainingMatrix",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_StaffTrainingMatrixList_MatrixId",
                table: "tbl_StaffTrainingMatrixList",
                column: "MatrixId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_BelieveTask");

            migrationBuilder.DropTable(
                name: "tbl_CareIssuesTask");

            migrationBuilder.DropTable(
                name: "tbl_FilesAndRecord");

            migrationBuilder.DropTable(
                name: "tbl_HealthTask");

            migrationBuilder.DropTable(
                name: "tbl_HealthTask2");

            migrationBuilder.DropTable(
                name: "tbl_StaffTrainingMatrixList");

            migrationBuilder.DropTable(
                name: "tbl_BestInterestAssessment");

            migrationBuilder.DropTable(
                name: "tbl_StaffTrainingMatrix");

            migrationBuilder.DropColumn(
                name: "ExpiryInMonths",
                table: "tbl_BaseRecordItem");

            migrationBuilder.AlterColumn<string>(
                name: "ValueName",
                table: "tbl_BaseRecordItem",
                type: "nvarchar(225)",
                maxLength: 225,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
