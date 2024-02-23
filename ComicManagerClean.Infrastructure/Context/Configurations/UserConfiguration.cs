using ComicManagerClean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComicManagerClean.Infrastructure.Context.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(user => user.Id);
        
        builder
            .Property(user => user.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder
            .Property(user => user.Password)
            .IsRequired()
            .HasMaxLength(128);

        builder
            .Property(user => user.Salt)
            .IsRequired()
            .HasMaxLength(128);
    }
}
