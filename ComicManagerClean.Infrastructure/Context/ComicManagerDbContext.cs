using ComicManagerClean.Domain.Entities;
using ComicManagerClean.Infrastructure.Context.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ComicManagerClean.Infrastructure.Context;

public class ComicManagerDbContext : DbContext, IComicManagerDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Comic> Comics { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<CharacterComic> CharacterComics { get; set; }
    

    public ComicManagerDbContext(DbContextOptions<ComicManagerDbContext> options)
            : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ComicManagerDbContext).Assembly);
    }
}
