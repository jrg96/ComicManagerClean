using ComicManagerClean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComicManagerClean.Infrastructure.Context.Configurations;

public class CharacterComicConfiguration : IEntityTypeConfiguration<CharacterComic>
{
    public void Configure(EntityTypeBuilder<CharacterComic> builder)
    {
        builder
            .HasKey(cc => new { cc.CharacterId, cc.ComicId });

        builder
            .HasOne<Character>(cc => cc.Character)
            .WithMany(character => character.CharacterComics)
            .HasForeignKey(cc => cc.CharacterId);

        builder
            .HasOne<Comic>(cc => cc.Comic)
            .WithMany(comic => comic.CharacterComics)
            .HasForeignKey(cc => cc.ComicId);
    }
}
