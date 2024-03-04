namespace ComicManagerClean.Domain.Entities;

public class CharacterComic
{
    public Character Character { get; set; }
    public Guid CharacterId { get; set; }

    public Comic Comic { get; set; }
    public Guid ComicId { get; set; }
}