using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learning4.Migrations.LeavesDb
{
    /// <inheritdoc />
    public partial class LeavesFirstMigraiton : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveTypeMasters",
                columns: table => new
                {
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypeMasters", x => x.LeaveTypeId);
                });

            migrationBuilder.CreateTable(
                name: "StatusMasters",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusMasters", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "LeavesMasters",
                columns: table => new
                {
                    LeaveId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    LeaveDays = table.Column<int>(type: "int", nullable: false),
                    LeaveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeavesMasters", x => x.LeaveId);
                    table.ForeignKey(
                        name: "FK_LeavesMasters_LeaveTypeMasters_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypeMasters",
                        principalColumn: "LeaveTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeavesMasters_StatusMasters_StatusId",
                        column: x => x.StatusId,
                        principalTable: "StatusMasters",
                        principalColumn: "StatusId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeavesMasters_LeaveTypeId",
                table: "LeavesMasters",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeavesMasters_StatusId",
                table: "LeavesMasters",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeavesMasters");

            migrationBuilder.DropTable(
                name: "LeaveTypeMasters");

            migrationBuilder.DropTable(
                name: "StatusMasters");
        }
    }
}
