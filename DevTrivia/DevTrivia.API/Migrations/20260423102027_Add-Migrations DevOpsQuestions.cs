using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevTrivia.API.Migrations
{
    /// <inheritdoc />
    public partial class AddMigrationsDevOpsQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "Questions",
            columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "Difficulty", "Title", "UpdatedAt" },
            values: new object[,]
            {
                { 172L, 104L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", 1, "What does the acronym \"CI\" mean in CI/CD?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 173L, 104L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", 1, "Which tool is widely known as an open-source automation platform used to build CI/CD pipelines?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 174L, 104L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "", 1, "What is a \"Container\" in the context of DevOps?", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
            });

            migrationBuilder.InsertData(
            table: "AnswerOptions",
            columns: new[] { "Id", "CreatedAt", "IsCorrect", "QuestionId", "Text", "UpdatedAt" },
            values: new object[,]
            {
                { 447L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 172L, "Continuous Infrastructure", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 448L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 172L, "Continuous Integration", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 449L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 172L, "Constant Iteration", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 450L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 172L, "Code Inspection", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 451L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 172L, "Cloud Integration", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 452L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 173L, "Photoshop", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 453L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 173L, "Jenkins", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 454L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 173L, "Figma", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 455L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 173L, "Trello", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 456L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 173L, "WordPress", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 457L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 174L, "A high capacity physical server", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 458L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 174L, "A programming language for infrastructure", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 459L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 174L, "A standard unit of software that packages code and all its dependencies to run quickly and reliably", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 460L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 174L, "A relational database", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 461L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, 174L, "A high-speed network cable", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
