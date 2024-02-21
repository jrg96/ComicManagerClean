using ComicManagerClean.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComicManagerClean.Infrastructure.Context.Contracts;

public interface IComicManagerDbContext
{
    public DbSet<User> Users { get; set; }
}
