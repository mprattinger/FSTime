using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSTime.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WorkscheduleLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkplanId",
                table: "Employees");

            migrationBuilder.CreateTable(
                name: "EmployeeWorkschedules",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkscheduleId = table.Column<Guid>(type: "uuid", nullable: false),
                    From = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    To = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeWorkschedules", x => new { x.EmployeeId, x.WorkscheduleId, x.From });
                    table.ForeignKey(
                        name: "FK_EmployeeWorkschedules_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeWorkschedules_WorkSchedules_WorkscheduleId",
                        column: x => x.WorkscheduleId,
                        principalTable: "WorkSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeWorkschedules_WorkscheduleId",
                table: "EmployeeWorkschedules",
                column: "WorkscheduleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeWorkschedules");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkplanId",
                table: "Employees",
                type: "uuid",
                nullable: true);
        }
    }
}
