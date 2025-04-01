using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSTime.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LinkSupervisor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Supervisor",
                table: "Employees",
                newName: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SupervisorId",
                table: "Employees",
                column: "SupervisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_SupervisorId",
                table: "Employees",
                column: "SupervisorId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_SupervisorId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_SupervisorId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "SupervisorId",
                table: "Employees",
                newName: "Supervisor");
        }
    }
}
