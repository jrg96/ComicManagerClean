namespace ComicManagerClean.Contracts.Comic;

public class UpdateComicRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int Chapters { get; set; }
}
