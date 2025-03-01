using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSTime.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class verified2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>("Verified", "Users", type: "boolean", nullable: false, defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
