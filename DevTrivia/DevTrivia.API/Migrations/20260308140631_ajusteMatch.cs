using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevTrivia.API.Migrations
{
    /// <inheritdoc />
    public partial class ajusteMatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartedAt",
                table: "Match",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datatime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndedAt",
                table: "Match",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datatime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartedAt",
                table: "Match",
                type: "datatime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndedAt",
                table: "Match",
                type: "datatime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");
        }
    }
}
