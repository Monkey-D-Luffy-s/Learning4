using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learning4.Migrations.CouponDb
{
    /// <inheritdoc />
    public partial class UpdatedEmployeeModel4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Singnature_DocumentPath",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Singnature_DocumentPath",
                table: "Employees");
        }
    }
}
