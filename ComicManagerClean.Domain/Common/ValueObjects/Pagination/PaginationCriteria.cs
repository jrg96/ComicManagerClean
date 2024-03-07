namespace ComicManagerClean.Domain.Common.ValueObjects.Pagination;

public class PaginationCriteria
{
    public int Page { get; set; }
    public int Size { get; set; }
    public string SortBy { get; set; }
    public bool Ascending { get; set; }
    public string SearchString { get; set; }
}
