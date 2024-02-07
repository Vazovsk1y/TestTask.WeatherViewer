using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTask.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasurementDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    AirTemperature = table.Column<double>(type: "float", nullable: false),
                    AirHumidity = table.Column<double>(type: "float", nullable: false),
                    DewPoint = table.Column<double>(type: "float", nullable: false),
                    AirPressure = table.Column<double>(type: "float", nullable: false),
                    WindSpeed = table.Column<double>(type: "float", nullable: true),
                    Clouds = table.Column<double>(type: "float", nullable: true),
                    LowCloudBoundary = table.Column<double>(type: "float", nullable: true),
                    HorizontalVisibility = table.Column<double>(type: "float", nullable: true),
                    NaturalPhenomena = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainWindDirection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondaryWindDirection = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherRecords", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherRecords");
        }
    }
}
