using ComicManagerClean.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComicManagerClean.Infrastructure.Context.Contracts;

public interface IComicManagerDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Comic> Comics { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<CharacterComic> CharacterComics { get; set; }
}
