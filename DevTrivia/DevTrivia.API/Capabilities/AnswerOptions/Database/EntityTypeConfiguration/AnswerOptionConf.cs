using DevTrivia.API.Capabilities.AnswerOptions.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTrivia.API.Capabilities.AnswerOptions.Database.EntityTypeConfiguration;

public class AnswerOptionConf : IEntityTypeConfiguration<AnswerOptionEntity>
{
    public void Configure(EntityTypeBuilder<AnswerOptionEntity> builder)
    {
        builder.ToTable("AnswerOptions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

        builder.Property(x => x.Text)
                .HasMaxLength(500)
                .IsRequired();

        builder.Property(x => x.IsCorrect)
                .IsRequired();

        builder.Property(x => x.QuestionId)
                .IsRequired();

        builder.HasOne(x => x.QuestionEntity)
                .WithMany(x => x.AnswerOptions)
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}