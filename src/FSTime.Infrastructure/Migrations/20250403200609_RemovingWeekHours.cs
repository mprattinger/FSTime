using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSTime.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovingWeekHours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeekWorkDays",
                table: "WorkSchedules");

            migrationBuilder.DropColumn(
                name: "WeekWorkTime",
                table: "WorkSchedules");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WeekWorkDays",
                table: "WorkSchedules",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "WeekWorkTime",
                table: "WorkSchedules",
                type: "double precision",
                nullable: true);
        }
    }
}
