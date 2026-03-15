using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevTrivia.API.Migrations;

/// <inheritdoc />
public partial class RenameAnswerQuestionsToAnswerOptions : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_AnswerQuestions_Questions_QuestionId",
            table: "AnswerQuestions");

        migrationBuilder.DropPrimaryKey(
            name: "PK_AnswerQuestions",
            table: "AnswerQuestions");

        migrationBuilder.RenameTable(
            name: "AnswerQuestions",
            newName: "AnswerOptions");

        migrationBuilder.RenameIndex(
            name: "IX_AnswerQuestions_QuestionId",
            table: "AnswerOptions",
            newName: "IX_AnswerOptions_QuestionId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_AnswerOptions",
            table: "AnswerOptions",
            column: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_AnswerOptions_Questions_QuestionId",
            table: "AnswerOptions",
            column: "QuestionId",
            principalTable: "Questions",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_AnswerOptions_Questions_QuestionId",
            table: "AnswerOptions");

        migrationBuilder.DropPrimaryKey(
            name: "PK_AnswerOptions",
            table: "AnswerOptions");

        migrationBuilder.RenameTable(
            name: "AnswerOptions",
            newName: "AnswerQuestions");

        migrationBuilder.RenameIndex(
            name: "IX_AnswerOptions_QuestionId",
            table: "AnswerQuestions",
            newName: "IX_AnswerQuestions_QuestionId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_AnswerQuestions",
            table: "AnswerQuestions",
            column: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_AnswerQuestions_Questions_QuestionId",
            table: "AnswerQuestions",
            column: "QuestionId",
            principalTable: "Questions",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}