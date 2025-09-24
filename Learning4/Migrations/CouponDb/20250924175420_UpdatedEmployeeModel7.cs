using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learning4.Migrations.CouponDb
{
    /// <inheritdoc />
    public partial class UpdatedEmployeeModel7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IsActive",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Y",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IsActive",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Y");
        }
    }
}
