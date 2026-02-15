using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace ElderCareApp.Migrations
{
    /// <inheritdoc />
    public partial class AddStaffAndHealth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: false),
                    Role = table.Column<string>(type: "longtext", nullable: false),
                    CareHomeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staffs_CareHomes_CareHomeId",
                        column: x => x.CareHomeId,
                        principalTable: "CareHomes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ElderHealthRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ElderId = table.Column<int>(type: "int", nullable: false),
                    BloodPressure = table.Column<string>(type: "longtext", nullable: false),
                    HeartRate = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "longtext", nullable: false),
                    DateRecorded = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElderHealthRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElderHealthRecords_Elders_ElderId",
                        column: x => x.ElderId,
                        principalTable: "Elders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElderHealthRecords_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ElderHealthRecords_ElderId",
                table: "ElderHealthRecords",
                column: "ElderId");

            migrationBuilder.CreateIndex(
                name: "IX_ElderHealthRecords_StaffId",
                table: "ElderHealthRecords",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_CareHomeId",
                table: "Staffs",
                column: "CareHomeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElderHealthRecords");

            migrationBuilder.DropTable(
                name: "Staffs");
        }
    }
}
