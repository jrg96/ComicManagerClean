namespace ComicManagerClean.Contracts.Comic;

public class CreateComicRequest
{
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int Chapters { get; set; }
}
