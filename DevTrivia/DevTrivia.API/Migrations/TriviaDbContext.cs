using DevTrivia.API.Capabilities.AnswerOptions.Database.Entities;
using DevTrivia.API.Capabilities.AnswerOptions.Database.EntityTypeConfiguration;
using DevTrivia.API.Capabilities.Category.Database.Entities;
using DevTrivia.API.Capabilities.Category.Database.EntityTypeConfiguration;
using DevTrivia.API.Capabilities.Match.Database.Entities;
using DevTrivia.API.Capabilities.Match.Database.EntityTypeConfiguration;
using DevTrivia.API.Capabilities.Question.Database.Entities;
using DevTrivia.API.Capabilities.Question.Database.EntityTypeConfiguration;
using DevTrivia.API.Capabilities.User.Database.Entities;
using DevTrivia.API.Capabilities.User.Database.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace DevTrivia.API.Migrations;

public class TriviaDbContext(DbContextOptions<TriviaDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<QuestionEntity> Questions { get; set; }
    public DbSet<AnswerOptionEntity> AnswerOptions { get; set; }
    public DbSet<MatchEntity> Matches { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure all DateTime properties to use UTC
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(
                        new Microsoft.EntityFrameworkCore.Storage.ValueConversion.ValueConverter<DateTime, DateTime>(
                            v => v.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(v, DateTimeKind.Utc) : v.ToUniversalTime(),
                            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                }
            }
        }

        Configure(modelBuilder);
    }

    private static void Configure(ModelBuilder modelBuilder)
    {
        new UserConf().Configure(modelBuilder.Entity<UserEntity>());
        new CategoryConf().Configure(modelBuilder.Entity<CategoryEntity>());
        new QuestionConf().Configure(modelBuilder.Entity<QuestionEntity>());
        new AnswerOptionConf().Configure(modelBuilder.Entity<AnswerOptionEntity>());
        new MatchConf().Configure(modelBuilder.Entity<MatchEntity>());
    }
}
