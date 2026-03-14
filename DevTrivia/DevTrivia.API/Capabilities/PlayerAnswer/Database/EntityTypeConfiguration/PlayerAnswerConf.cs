using DevTrivia.API.Capabilities.PlayerAnswer.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTrivia.API.Capabilities.PlayerAnswer.Database.EntityTypeConfiguration;

public class PlayerAnswerConf : IEntityTypeConfiguration<PlayerAnswerEntity>
{
    public void Configure(EntityTypeBuilder<PlayerAnswerEntity> builder)
    {
        builder.ToTable("PlayerAnswers");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.MatchId)
            .IsRequired();

        builder.Property(x => x.QuestionId)
            .IsRequired();

        builder.Property(x => x.IsCorrect)
            .IsRequired();

        builder.Property(x => x.AnsweredAt)
            .HasColumnType("timestamp with time zone");

        builder.HasIndex(x => new { x.MatchId, x.QuestionId })
            .IsUnique();

        builder.HasOne(x => x.Match)
            .WithMany(x => x.PlayerAnswers)
            .HasForeignKey(x => x.MatchId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Question)
            .WithMany()
            .HasForeignKey(x => x.QuestionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.SelectedAnswerOption)
            .WithMany()
            .HasForeignKey(x => x.SelectedAnswerOptionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
