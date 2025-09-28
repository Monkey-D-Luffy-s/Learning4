using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learning4.Migrations.LeavesDb
{
    /// <inheritdoc />
    public partial class LeavesSecondMigraiton : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "LeavesMasters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "LeavesMasters");
        }
    }
}
