using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSTime.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingIsHead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHead",
                table: "Employees",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHead",
                table: "Employees");
        }
    }
}
