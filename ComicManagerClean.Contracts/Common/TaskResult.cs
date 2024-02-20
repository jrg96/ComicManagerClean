namespace ComicManagerClean.Contracts.Common;

public class TaskResult<T> where T : class
{
    public IEnumerable<string> ErrorList { get; set; }
    public bool Successful { get; set; }
    public T Data { get; set; }
}
