namespace ComicManagerClean.Contracts.Common;

public class TaskResult
{
    public IEnumerable<string> ErrorList { get; set; }
    public bool Successful { get; set; }
}

public class TaskResult<T> : TaskResult where T : class
{
    public T Data { get; set; }
}
