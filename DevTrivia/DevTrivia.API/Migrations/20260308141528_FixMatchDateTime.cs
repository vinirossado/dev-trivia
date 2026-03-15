using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevTrivia.API.Migrations;

/// <inheritdoc />
public partial class FixMatchDateTime : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "StartedAt",
            table: "Match",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp");

        migrationBuilder.AlterColumn<DateTime>(
            name: "EndedAt",
            table: "Match",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "StartedAt",
            table: "Match",
            type: "timestamp",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            name: "EndedAt",
            table: "Match",
            type: "timestamp",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");
    }
}