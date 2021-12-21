using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class StaffPersonalInfo_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Staff_AdlObs_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "tbl_Staff_AdlObs");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Staff_KeyWorkerVoice_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "tbl_Staff_KeyWorkerVoice");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Staff_MedCompObs_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "tbl_Staff_MedCompObs");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Staff_OneToOne_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "tbl_Staff_OneToOne");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Staff_Reference_tbl_StaffPersonalInfo_ConfirmedBy",
                table: "tbl_Staff_Reference");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Staff_SpotCheck_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "tbl_Staff_SpotCheck");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Staff_SupervisionAppraisal_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "tbl_Staff_SupervisionAppraisal");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Staff_Survey_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "tbl_Staff_Survey");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Staff_Survey_StaffPersonalInfoId",
                table: "tbl_Staff_Survey");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Staff_SupervisionAppraisal_StaffPersonalInfoId",
                table: "tbl_Staff_SupervisionAppraisal");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Staff_SpotCheck_StaffPersonalInfoId",
                table: "tbl_Staff_SpotCheck");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Staff_Reference_ConfirmedBy",
                table: "tbl_Staff_Reference");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Staff_OneToOne_StaffPersonalInfoId",
                table: "tbl_Staff_OneToOne");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Staff_MedCompObs_StaffPersonalInfoId",
                table: "tbl_Staff_MedCompObs");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Staff_KeyWorkerVoice_StaffPersonalInfoId",
                table: "tbl_Staff_KeyWorkerVoice");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Staff_AdlObs_StaffPersonalInfoId",
                table: "tbl_Staff_AdlObs");

            migrationBuilder.DropColumn(
                name: "StaffPersonalInfoId",
                table: "tbl_Staff_Survey");

            migrationBuilder.DropColumn(
                name: "StaffPersonalInfoId",
                table: "tbl_Staff_SupervisionAppraisal");

            migrationBuilder.DropColumn(
                name: "StaffPersonalInfoId",
                table: "tbl_Staff_SpotCheck");

            migrationBuilder.DropColumn(
                name: "StaffPersonalInfoId",
                table: "tbl_Staff_OneToOne");

            migrationBuilder.DropColumn(
                name: "StaffPersonalInfoId",
                table: "tbl_Staff_MedCompObs");

            migrationBuilder.DropColumn(
                name: "StaffPersonalInfoId",
                table: "tbl_Staff_KeyWorkerVoice");

            migrationBuilder.DropColumn(
                name: "StaffPersonalInfoId",
                table: "tbl_Staff_AdlObs");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_Survey_StaffId",
                table: "tbl_Staff_Survey",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_SupervisionAppraisal_StaffId",
                table: "tbl_Staff_SupervisionAppraisal",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_SpotCheck_StaffId",
                table: "tbl_Staff_SpotCheck",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_Reference_StaffId",
                table: "tbl_Staff_Reference",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_OneToOne_StaffId",
                table: "tbl_Staff_OneToOne",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_MedCompObs_StaffId",
                table: "tbl_Staff_MedCompObs",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_KeyWorkerVoice_StaffId",
                table: "tbl_Staff_KeyWorkerVoice",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_AdlObs_StaffId",
                table: "tbl_Staff_AdlObs",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Staff_AdlObs_tbl_StaffPersonalInfo_StaffId",
                table: "tbl_Staff_AdlObs",
                column: "StaffId",
                principalTable: "tbl_StaffPersonalInfo",
                principalColumn: "StaffPersonalInfoId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Staff_KeyWorkerVoice_tbl_StaffPersonalInfo_StaffId",
                table: "tbl_Staff_KeyWorkerVoice",
                column: "StaffId",
                principalTable: "tbl_StaffPersonalInfo",
                principalColumn: "StaffPersonalInfoId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Staff_MedCompObs_tbl_StaffPersonalInfo_StaffId",
                table: "tbl_Staff_MedCompObs",
                column: "StaffId",
                principalTable: "tbl_StaffPersonalInfo",
                principalColumn: "StaffPersonalInfoId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Staff_OneToOne_tbl_StaffPersonalInfo_StaffId",
                table: "tbl_Staff_OneToOne",
                column: "StaffId",
                principalTable: "tbl_StaffPersonalInfo",
                principalColumn: "StaffPersonalInfoId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Staff_Reference_tbl_StaffPersonalInfo_StaffId",
                table: "tbl_Staff_Reference",
                column: "StaffId",
                principalTable: "tbl_StaffPersonalInfo",
                principalColumn: "StaffPersonalInfoId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Staff_SpotCheck_tbl_StaffPersonalInfo_StaffId",
                table: "tbl_Staff_SpotCheck",
                column: "StaffId",
                principalTable: "tbl_StaffPersonalInfo",
                principalColumn: "StaffPersonalInfoId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Staff_SupervisionAppraisal_tbl_StaffPersonalInfo_StaffId",
                table: "tbl_Staff_SupervisionAppraisal",
                column: "StaffId",
                principalTable: "tbl_StaffPersonalInfo",
                principalColumn: "StaffPersonalInfoId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Staff_Survey_tbl_StaffPersonalInfo_StaffId",
                table: "tbl_Staff_Survey",
                column: "StaffId",
                principalTable: "tbl_StaffPersonalInfo",
                principalColumn: "StaffPersonalInfoId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Staff_AdlObs_tbl_StaffPersonalInfo_StaffId",
                table: "tbl_Staff_AdlObs");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Staff_KeyWorkerVoice_tbl_StaffPersonalInfo_StaffId",
                table: "tbl_Staff_KeyWorkerVoice");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Staff_MedCompObs_tbl_StaffPersonalInfo_StaffId",
                table: "tbl_Staff_MedCompObs");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Staff_OneToOne_tbl_StaffPersonalInfo_StaffId",
                table: "tbl_Staff_OneToOne");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Staff_Reference_tbl_StaffPersonalInfo_StaffId",
                table: "tbl_Staff_Reference");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Staff_SpotCheck_tbl_StaffPersonalInfo_StaffId",
                table: "tbl_Staff_SpotCheck");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Staff_SupervisionAppraisal_tbl_StaffPersonalInfo_StaffId",
                table: "tbl_Staff_SupervisionAppraisal");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Staff_Survey_tbl_StaffPersonalInfo_StaffId",
                table: "tbl_Staff_Survey");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Staff_Survey_StaffId",
                table: "tbl_Staff_Survey");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Staff_SupervisionAppraisal_StaffId",
                table: "tbl_Staff_SupervisionAppraisal");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Staff_SpotCheck_StaffId",
                table: "tbl_Staff_SpotCheck");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Staff_Reference_StaffId",
                table: "tbl_Staff_Reference");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Staff_OneToOne_StaffId",
                table: "tbl_Staff_OneToOne");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Staff_MedCompObs_StaffId",
                table: "tbl_Staff_MedCompObs");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Staff_KeyWorkerVoice_StaffId",
                table: "tbl_Staff_KeyWorkerVoice");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Staff_AdlObs_StaffId",
                table: "tbl_Staff_AdlObs");

            migrationBuilder.AddColumn<int>(
                name: "StaffPersonalInfoId",
                table: "tbl_Staff_Survey",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StaffPersonalInfoId",
                table: "tbl_Staff_SupervisionAppraisal",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StaffPersonalInfoId",
                table: "tbl_Staff_SpotCheck",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StaffPersonalInfoId",
                table: "tbl_Staff_OneToOne",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StaffPersonalInfoId",
                table: "tbl_Staff_MedCompObs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StaffPersonalInfoId",
                table: "tbl_Staff_KeyWorkerVoice",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StaffPersonalInfoId",
                table: "tbl_Staff_AdlObs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_Survey_StaffPersonalInfoId",
                table: "tbl_Staff_Survey",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_SupervisionAppraisal_StaffPersonalInfoId",
                table: "tbl_Staff_SupervisionAppraisal",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_SpotCheck_StaffPersonalInfoId",
                table: "tbl_Staff_SpotCheck",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_Reference_ConfirmedBy",
                table: "tbl_Staff_Reference",
                column: "ConfirmedBy");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_OneToOne_StaffPersonalInfoId",
                table: "tbl_Staff_OneToOne",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_MedCompObs_StaffPersonalInfoId",
                table: "tbl_Staff_MedCompObs",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_KeyWorkerVoice_StaffPersonalInfoId",
                table: "tbl_Staff_KeyWorkerVoice",
                column: "StaffPersonalInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Staff_AdlObs_StaffPersonalInfoId",
                table: "tbl_Staff_AdlObs",
                column: "StaffPersonalInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Staff_AdlObs_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "tbl_Staff_AdlObs",
                column: "StaffPersonalInfoId",
                principalTable: "tbl_StaffPersonalInfo",
                principalColumn: "StaffPersonalInfoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Staff_KeyWorkerVoice_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "tbl_Staff_KeyWorkerVoice",
                column: "StaffPersonalInfoId",
                principalTable: "tbl_StaffPersonalInfo",
                principalColumn: "StaffPersonalInfoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Staff_MedCompObs_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "tbl_Staff_MedCompObs",
                column: "StaffPersonalInfoId",
                principalTable: "tbl_StaffPersonalInfo",
                principalColumn: "StaffPersonalInfoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Staff_OneToOne_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "tbl_Staff_OneToOne",
                column: "StaffPersonalInfoId",
                principalTable: "tbl_StaffPersonalInfo",
                principalColumn: "StaffPersonalInfoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Staff_Reference_tbl_StaffPersonalInfo_ConfirmedBy",
                table: "tbl_Staff_Reference",
                column: "ConfirmedBy",
                principalTable: "tbl_StaffPersonalInfo",
                principalColumn: "StaffPersonalInfoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Staff_SpotCheck_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "tbl_Staff_SpotCheck",
                column: "StaffPersonalInfoId",
                principalTable: "tbl_StaffPersonalInfo",
                principalColumn: "StaffPersonalInfoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Staff_SupervisionAppraisal_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "tbl_Staff_SupervisionAppraisal",
                column: "StaffPersonalInfoId",
                principalTable: "tbl_StaffPersonalInfo",
                principalColumn: "StaffPersonalInfoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Staff_Survey_tbl_StaffPersonalInfo_StaffPersonalInfoId",
                table: "tbl_Staff_Survey",
                column: "StaffPersonalInfoId",
                principalTable: "tbl_StaffPersonalInfo",
                principalColumn: "StaffPersonalInfoId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
