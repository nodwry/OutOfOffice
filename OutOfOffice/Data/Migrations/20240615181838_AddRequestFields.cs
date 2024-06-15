using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutOfOffice.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRequestFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_PeoplePartnerId",
                table: "Employees");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastStatusChange",
                table: "LeaveRequests",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedTime",
                table: "LeaveRequests",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "PeoplePartnerId",
                table: "Employees",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastStatusChange",
                table: "ApprovalRequests",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_PeoplePartnerId",
                table: "Employees",
                column: "PeoplePartnerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_PeoplePartnerId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "LastStatusChange",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "SubmittedTime",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "LastStatusChange",
                table: "ApprovalRequests");

            migrationBuilder.AlterColumn<int>(
                name: "PeoplePartnerId",
                table: "Employees",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_PeoplePartnerId",
                table: "Employees",
                column: "PeoplePartnerId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
