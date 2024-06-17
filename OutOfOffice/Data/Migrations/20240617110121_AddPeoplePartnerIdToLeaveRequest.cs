using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutOfOffice.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPeoplePartnerIdToLeaveRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeoplePartnerId",
                table: "LeaveRequests",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeoplePartnerId",
                table: "LeaveRequests");
        }
    }
}
