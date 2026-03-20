using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElderCareApp.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToElder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Elders",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Elders");
        }
    }
}
