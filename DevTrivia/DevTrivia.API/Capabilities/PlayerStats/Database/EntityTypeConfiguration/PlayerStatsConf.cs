using DevTrivia.API.Capabilities.PlayerStats.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTrivia.API.Capabilities.PlayerStats.Database.EntityTypeConfiguration;

public class PlayerStatsConf : IEntityTypeConfiguration<PlayerStatsEntity>
{
    public void Configure(EntityTypeBuilder<PlayerStatsEntity> builder)
    {
        builder.ToTable("PlayerStats");

        builder.Property(x => x.UserId)
            .IsRequired();
        
        builder.Property(x => x.TotalMatches)
            .IsRequired()
            .HasColumnType("integer");

        builder.Property(x => x.TotalCorrect)
            .IsRequired()
            .HasColumnType("integer");

        builder.Property(x => x.EloRating)
            .IsRequired()
            .HasMaxLength(20);
        
    }
}
