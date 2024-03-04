namespace ComicManagerClean.Domain.Entities;

public class Comic
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int Chapters { get; set; }

    // Navigation properties
    public IList<CharacterComic> CharacterComics { get; set; }
}
