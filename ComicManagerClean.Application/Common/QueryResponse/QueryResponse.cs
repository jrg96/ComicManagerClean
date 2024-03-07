namespace ComicManagerClean.Application.Common.QueryResponse;

public class QueryResponse<T> where T : class
{
    public int TotalCount { get; set; }
    public IEnumerable<T> Data { get; set; }
}
