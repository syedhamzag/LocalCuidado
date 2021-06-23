using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffRotaPeriod_StartTime_StopTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tbl_Client_ComplainRegister_ComplainId",
                table: "tbl_Client_ComplainRegister");

            migrationBuilder.AddColumn<string>(
                name: "StartTime",
                table: "tbl_StaffRotaPeriod",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StopTime",
                table: "tbl_StaffRotaPeriod",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SOURCEOFCOMPLAINTS",
                table: "tbl_Client_ComplainRegister",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ROOTCAUSE",
                table: "tbl_Client_ComplainRegister",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "REMARK",
                table: "tbl_Client_ComplainRegister",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LINK",
                table: "tbl_Client_ComplainRegister",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LETTERTOSTAFF",
                table: "tbl_Client_ComplainRegister",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IRFNUMBER ",
                table: "tbl_Client_ComplainRegister",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "INVESTIGATIONOUTCOME",
                table: "tbl_Client_ComplainRegister",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FINALRESPONSETOFAMILY",
                table: "tbl_Client_ComplainRegister",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EvidenceFilePath",
                table: "tbl_Client_ComplainRegister",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATEOFACKNOWLEDGEMENT",
                table: "tbl_Client_ComplainRegister",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CONCERNSRAISED",
                table: "tbl_Client_ComplainRegister",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "COMPLAINANTCONTACT",
                table: "tbl_Client_ComplainRegister",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ACTIONTAKEN",
                table: "tbl_Client_ComplainRegister",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "tbl_StaffRotaPeriod");

            migrationBuilder.DropColumn(
                name: "StopTime",
                table: "tbl_StaffRotaPeriod");

            migrationBuilder.AlterColumn<string>(
                name: "SOURCEOFCOMPLAINTS",
                table: "tbl_Client_ComplainRegister",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "ROOTCAUSE",
                table: "tbl_Client_ComplainRegister",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "REMARK",
                table: "tbl_Client_ComplainRegister",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "LINK",
                table: "tbl_Client_ComplainRegister",
                type: "nvarchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "LETTERTOSTAFF",
                table: "tbl_Client_ComplainRegister",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "IRFNUMBER ",
                table: "tbl_Client_ComplainRegister",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "INVESTIGATIONOUTCOME",
                table: "tbl_Client_ComplainRegister",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "FINALRESPONSETOFAMILY",
                table: "tbl_Client_ComplainRegister",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "EvidenceFilePath",
                table: "tbl_Client_ComplainRegister",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATEOFACKNOWLEDGEMENT",
                table: "tbl_Client_ComplainRegister",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "CONCERNSRAISED",
                table: "tbl_Client_ComplainRegister",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "COMPLAINANTCONTACT",
                table: "tbl_Client_ComplainRegister",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ACTIONTAKEN",
                table: "tbl_Client_ComplainRegister",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Client_ComplainRegister_ComplainId",
                table: "tbl_Client_ComplainRegister",
                column: "ComplainId");
        }
    }
}
