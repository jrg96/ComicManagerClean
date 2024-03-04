using ComicManagerClean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComicManagerClean.Infrastructure.Context.Configurations;

public class CharacterConfiguration : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(character => character.HeroName)
            .IsRequired()
            .HasMaxLength(200);

        builder
            .Property(character => character.FirstName)
            .IsRequired()
            .HasMaxLength(200);

        builder
            .Property(character => character.LastName)
            .IsRequired()
            .HasMaxLength(200);

        builder
            .Property(character => character.DateOfBirth)
            .IsRequired();

        builder
            .Property(character => character.Deceased)
            .IsRequired()
            .HasDefaultValue(false);

        builder
            .Property(character => character.CharacterType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(64);
    }
}
