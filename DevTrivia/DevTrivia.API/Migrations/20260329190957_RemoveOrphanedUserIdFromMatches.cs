using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevTrivia.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOrphanedUserIdFromMatches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE "Matches" DROP CONSTRAINT IF EXISTS "FK_Matches_Users_UserId";
                DROP INDEX IF EXISTS "IX_Matches_UserId";
                ALTER TABLE "Matches" DROP COLUMN IF EXISTS "UserId";
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
