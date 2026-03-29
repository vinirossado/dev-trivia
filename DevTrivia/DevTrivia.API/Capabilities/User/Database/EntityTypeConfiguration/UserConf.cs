using DevTrivia.API.Capabilities.User.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevTrivia.API.Capabilities.User.Database.EntityTypeConfiguration;

public class UserConf : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.PasswordHash)
            .HasMaxLength(255);

        builder.Property(x => x.Role)
            .IsRequired()
            .HasDefaultValue(Enums.RoleEnum.Player);

        builder.Property(x => x.AuthProvider)
            .IsRequired()
            .HasMaxLength(50)
            .HasDefaultValue("local");

        builder.Property(x => x.ExternalId)
            .HasMaxLength(255);

        builder.Property(x => x.Bio)
            .HasMaxLength(150);

        builder.Property(x => x.Location)
            .HasMaxLength(100);

        builder.Property(x => x.DateOfBirth);

        builder.Property(x => x.LastLoginAt);

        builder.Property(x => x.PreferredLanguage)
            .HasMaxLength(10);

        // Indexes
        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.HasIndex(x => new { x.AuthProvider, x.ExternalId })
            .IsUnique();
        
        builder.HasMany(x => x.Matches)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);
        
        builder.HasMany(x => x.PlayerStats)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);
    }
}