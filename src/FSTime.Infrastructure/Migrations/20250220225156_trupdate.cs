using Microsoft.EntityFrameworkCore.Migrations;
#pragma warning disable CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
#nullable disable

namespace FSTime.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class trupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TenantRoles_UserId",
                table: "TenantRoles",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TenantRoles_Tenants_TenantId",
                table: "TenantRoles",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TenantRoles_Users_UserId",
                table: "TenantRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TenantRoles_Tenants_TenantId",
                table: "TenantRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_TenantRoles_Users_UserId",
                table: "TenantRoles");

            migrationBuilder.DropIndex(
                name: "IX_TenantRoles_UserId",
                table: "TenantRoles");
        }
    }
}
