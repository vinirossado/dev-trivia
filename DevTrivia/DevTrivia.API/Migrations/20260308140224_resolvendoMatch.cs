using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevTrivia.API.Migrations;

/// <inheritdoc />
public partial class resolvendoMatch : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_MatchEntity_Categories_CategoryId",
            table: "MatchEntity");

        migrationBuilder.DropPrimaryKey(
            name: "PK_MatchEntity",
            table: "MatchEntity");

        migrationBuilder.DropIndex(
            name: "IX_MatchEntity_CategoryId",
            table: "MatchEntity");

        migrationBuilder.DropColumn(
            name: "CategoryId",
            table: "MatchEntity");

        migrationBuilder.RenameTable(
            name: "MatchEntity",
            newName: "Match");

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

        migrationBuilder.AddPrimaryKey(
            name: "PK_Match",
            table: "Match",
            column: "Id");

        migrationBuilder.CreateIndex(
            name: "IX_Match_SelectedCategoryId",
            table: "Match",
            column: "SelectedCategoryId");

        migrationBuilder.AddForeignKey(
            name: "FK_Match_Categories_SelectedCategoryId",
            table: "Match",
            column: "SelectedCategoryId",
            principalTable: "Categories",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Match_Categories_SelectedCategoryId",
            table: "Match");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Match",
            table: "Match");

        migrationBuilder.DropIndex(
            name: "IX_Match_SelectedCategoryId",
            table: "Match");

        migrationBuilder.RenameTable(
            name: "Match",
            newName: "MatchEntity");

        migrationBuilder.AlterColumn<DateTime>(
            name: "StartedAt",
            table: "MatchEntity",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datatime");

        migrationBuilder.AlterColumn<DateTime>(
            name: "EndedAt",
            table: "MatchEntity",
            type: "timestamp with time zone",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datatime");

        migrationBuilder.AddColumn<long>(
            name: "CategoryId",
            table: "MatchEntity",
            type: "bigint",
            nullable: false,
            defaultValue: 0L);

        migrationBuilder.AddPrimaryKey(
            name: "PK_MatchEntity",
            table: "MatchEntity",
            column: "Id");

        migrationBuilder.CreateIndex(
            name: "IX_MatchEntity_CategoryId",
            table: "MatchEntity",
            column: "CategoryId");

        migrationBuilder.AddForeignKey(
            name: "FK_MatchEntity_Categories_CategoryId",
            table: "MatchEntity",
            column: "CategoryId",
            principalTable: "Categories",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}