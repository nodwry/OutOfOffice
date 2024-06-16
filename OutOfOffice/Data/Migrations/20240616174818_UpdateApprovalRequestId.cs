using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutOfOffice.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApprovalRequestId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ApprovalRequests",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ApprovalRequests",
                newName: "ID");
        }
    }
}
