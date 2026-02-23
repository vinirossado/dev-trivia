using DevTrivia.API.Capabilities.Question.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTrivia.API.Capabilities.Question.Database.EntityTypeConfiguration;

public class QuestionConf : IEntityTypeConfiguration<QuestionEntity>
{
    public void Configure(EntityTypeBuilder<QuestionEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Difficulty)
            .HasMaxLength(20)
            .HasColumnType("integer")
            .IsRequired();

        builder.Property(x => x.CategoryId)
            .IsRequired();

        // Relationships
        builder.HasOne(x => x.Category)
            .WithMany(x => x.Questions)
            .HasForeignKey(x => x.CategoryId);
    }
}