using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AwesomeCare.DataAccess.Migrations
{
    public partial class tbl_StaffRotaPeriod_StartTime_StopTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tbl_ClientComplainRegister_ComplainId",
                table: "tbl_ClientComplainRegister");

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
                table: "tbl_ClientComplainRegister",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ROOTCAUSE",
                table: "tbl_ClientComplainRegister",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "REMARK",
                table: "tbl_ClientComplainRegister",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LINK",
                table: "tbl_ClientComplainRegister",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LETTERTOSTAFF",
                table: "tbl_ClientComplainRegister",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IRFNUMBER ",
                table: "tbl_ClientComplainRegister",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "INVESTIGATIONOUTCOME",
                table: "tbl_ClientComplainRegister",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FINALRESPONSETOFAMILY",
                table: "tbl_ClientComplainRegister",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EvidenceFilePath",
                table: "tbl_ClientComplainRegister",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATEOFACKNOWLEDGEMENT",
                table: "tbl_ClientComplainRegister",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CONCERNSRAISED",
                table: "tbl_ClientComplainRegister",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "COMPLAINANTCONTACT",
                table: "tbl_ClientComplainRegister",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ACTIONTAKEN",
                table: "tbl_ClientComplainRegister",
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
                table: "tbl_ClientComplainRegister",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "ROOTCAUSE",
                table: "tbl_ClientComplainRegister",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "REMARK",
                table: "tbl_ClientComplainRegister",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "LINK",
                table: "tbl_ClientComplainRegister",
                type: "nvarchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "LETTERTOSTAFF",
                table: "tbl_ClientComplainRegister",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "IRFNUMBER ",
                table: "tbl_ClientComplainRegister",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "INVESTIGATIONOUTCOME",
                table: "tbl_ClientComplainRegister",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "FINALRESPONSETOFAMILY",
                table: "tbl_ClientComplainRegister",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "EvidenceFilePath",
                table: "tbl_ClientComplainRegister",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATEOFACKNOWLEDGEMENT",
                table: "tbl_ClientComplainRegister",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "CONCERNSRAISED",
                table: "tbl_ClientComplainRegister",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "COMPLAINANTCONTACT",
                table: "tbl_ClientComplainRegister",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ACTIONTAKEN",
                table: "tbl_ClientComplainRegister",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ClientComplainRegister_ComplainId",
                table: "tbl_ClientComplainRegister",
                column: "ComplainId");
        }
    }
}
