using DevTrivia.API.Capabilities.Category.Database.Entities;
using DevTrivia.API.Capabilities.Question.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTrivia.API.Capabilities.Question.Database.EntityTypeConfiguration;

public class QuestionConfg : IEntityTypeConfiguration<Entities.Question>
{
    public void Configure(EntityTypeBuilder<Entities.Question> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Ins).IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Difficulty)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.CategoryId)
            .IsRequired();
    }
}