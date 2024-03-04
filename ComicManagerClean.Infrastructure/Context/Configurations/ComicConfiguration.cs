using ComicManagerClean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComicManagerClean.Infrastructure.Context.Configurations;

public class ComicConfiguration : IEntityTypeConfiguration<Comic>
{
    public void Configure(EntityTypeBuilder<Comic> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(comic => comic.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder
            .Property(comic => comic.ReleaseDate)
            .IsRequired();

        builder
            .Property(comic => comic.Chapters)
            .IsRequired()
            .HasDefaultValue(1);
    }
}
