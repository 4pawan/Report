using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Report.Web.Migrations
{
    /// <inheritdoc />
    public partial class addniftyweeklytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NiftyWeekly",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Open = table.Column<double>(type: "float", nullable: false),
                    High = table.Column<double>(type: "float", nullable: false),
                    Low = table.Column<double>(type: "float", nullable: false),
                    Close = table.Column<double>(type: "float", nullable: false),
                    DayLowToHigh = table.Column<double>(type: "float", nullable: false),
                    PrevDayClose = table.Column<double>(type: "float", nullable: false),
                    HighFrmY = table.Column<double>(type: "float", nullable: false),
                    LowFrmY = table.Column<double>(type: "float", nullable: false),
                    CloseFrmY = table.Column<double>(type: "float", nullable: false),
                    _10AM = table.Column<double>(type: "float", nullable: false),
                    _1030AM = table.Column<double>(name: "_10_30AM", type: "float", nullable: false),
                    _1PM = table.Column<double>(type: "float", nullable: false),
                    _2PM = table.Column<double>(type: "float", nullable: false),
                    _225PM = table.Column<double>(name: "_2_25PM", type: "float", nullable: false),
                    Gap = table.Column<double>(type: "float", nullable: false),
                    DayMaxHighReachedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayMaxLowReachedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NiftyWeekly", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NiftyWeekly");
        }
    }
}
