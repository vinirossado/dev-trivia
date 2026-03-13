using DevTrivia.API.Capabilities.Match.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTrivia.API.Capabilities.Match.Database.EntityTypeConfiguration;

public class MatchConf : IEntityTypeConfiguration<MatchEntity>
{
    public void Configure(EntityTypeBuilder<MatchEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.StartedAt)
            .IsRequired()
            .HasColumnType("timestamp with time zone");

        builder.Property(x => x.EndedAt)
            .IsRequired()
            .HasColumnType("timestamp with time zone");

        builder.Property(x => x.Status)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.SelectedCategoryId)
            .IsRequired();

        // Relationships
        builder.HasOne(x => x.Category)
            .WithMany(x => x.Matches)
            .HasForeignKey(x => x.SelectedCategoryId);
    }
}
