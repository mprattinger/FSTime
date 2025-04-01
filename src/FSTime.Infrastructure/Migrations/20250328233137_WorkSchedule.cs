using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSTime.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WorkSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    WeekWorkTime = table.Column<double>(type: "double precision", nullable: true),
                    WeekWorkDays = table.Column<int>(type: "integer", nullable: true),
                    Monday = table.Column<double>(type: "double precision", nullable: true),
                    Tuesday = table.Column<double>(type: "double precision", nullable: true),
                    Wednesday = table.Column<double>(type: "double precision", nullable: true),
                    Thursday = table.Column<double>(type: "double precision", nullable: true),
                    Friday = table.Column<double>(type: "double precision", nullable: true),
                    Saturday = table.Column<double>(type: "double precision", nullable: true),
                    Sunday = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkSchedules_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkSchedules_CompanyId",
                table: "WorkSchedules",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkSchedules");
        }
    }
}
