using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevTrivia.API.Migrations
{
    /// <inheritdoc />
    public partial class Removingdateproperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndedAt",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "StartedAt",
                table: "Matches");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndedAt",
                table: "Matches",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartedAt",
                table: "Matches",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
