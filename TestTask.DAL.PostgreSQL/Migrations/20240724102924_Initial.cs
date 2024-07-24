using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTask.DAL.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherArchives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateAdded = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    LocalityTitle = table.Column<string>(type: "text", nullable: false),
                    LocalityTimeZoneId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherArchives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WeatherArchiveId = table.Column<Guid>(type: "uuid", nullable: false),
                    MeasurementDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    AirTemperature = table.Column<double>(type: "double precision", nullable: false),
                    AirHumidity = table.Column<double>(type: "double precision", nullable: false),
                    DewPoint = table.Column<double>(type: "double precision", nullable: false),
                    AirPressure = table.Column<double>(type: "double precision", nullable: false),
                    WindSpeed = table.Column<double>(type: "double precision", nullable: true),
                    Clouds = table.Column<double>(type: "double precision", nullable: true),
                    LowCloudBoundary = table.Column<double>(type: "double precision", nullable: true),
                    HorizontalVisibility = table.Column<double>(type: "double precision", nullable: true),
                    WeatherPhenomena = table.Column<string>(type: "text", nullable: true),
                    WindDirection = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherRecords_WeatherArchives_WeatherArchiveId",
                        column: x => x.WeatherArchiveId,
                        principalTable: "WeatherArchives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherRecords_WeatherArchiveId",
                table: "WeatherRecords",
                column: "WeatherArchiveId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherRecords");

            migrationBuilder.DropTable(
                name: "WeatherArchives");
        }
    }
}
