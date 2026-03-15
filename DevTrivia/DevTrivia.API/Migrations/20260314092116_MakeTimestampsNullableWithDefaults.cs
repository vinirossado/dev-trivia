using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevTrivia.API.Migrations;

/// <inheritdoc />
public partial class MakeTimestampsNullableWithDefaults : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedAt",
            table: "Users",
            type: "timestamp with time zone",
            nullable: true,
            defaultValueSql: "NOW()",
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "Users",
            type: "timestamp with time zone",
            nullable: true,
            defaultValueSql: "NOW()",
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedAt",
            table: "Questions",
            type: "timestamp with time zone",
            nullable: true,
            defaultValueSql: "NOW()",
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "Questions",
            type: "timestamp with time zone",
            nullable: true,
            defaultValueSql: "NOW()",
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedAt",
            table: "PlayerAnswers",
            type: "timestamp with time zone",
            nullable: true,
            defaultValueSql: "NOW()",
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "PlayerAnswers",
            type: "timestamp with time zone",
            nullable: true,
            defaultValueSql: "NOW()",
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedAt",
            table: "Matches",
            type: "timestamp with time zone",
            nullable: true,
            defaultValueSql: "NOW()",
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "Matches",
            type: "timestamp with time zone",
            nullable: true,
            defaultValueSql: "NOW()",
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedAt",
            table: "Categories",
            type: "timestamp with time zone",
            nullable: true,
            defaultValueSql: "NOW()",
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "Categories",
            type: "timestamp with time zone",
            nullable: true,
            defaultValueSql: "NOW()",
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedAt",
            table: "AnswerOptions",
            type: "timestamp with time zone",
            nullable: true,
            defaultValueSql: "NOW()",
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "AnswerOptions",
            type: "timestamp with time zone",
            nullable: true,
            defaultValueSql: "NOW()",
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedAt",
            table: "Users",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValueSql: "NOW()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "Users",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValueSql: "NOW()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedAt",
            table: "Questions",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValueSql: "NOW()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "Questions",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValueSql: "NOW()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedAt",
            table: "PlayerAnswers",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValueSql: "NOW()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "PlayerAnswers",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValueSql: "NOW()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedAt",
            table: "Matches",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValueSql: "NOW()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "Matches",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValueSql: "NOW()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedAt",
            table: "Categories",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValueSql: "NOW()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "Categories",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValueSql: "NOW()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedAt",
            table: "AnswerOptions",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValueSql: "NOW()");

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedAt",
            table: "AnswerOptions",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "timestamp with time zone",
            oldNullable: true,
            oldDefaultValueSql: "NOW()");
    }
}