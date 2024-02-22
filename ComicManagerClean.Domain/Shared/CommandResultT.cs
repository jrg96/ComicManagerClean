namespace ComicManagerClean.Domain.Shared;

public class CommandResult<T> : CommandResult where T : class
{
    private readonly T _value;
    public T Value { get => _value; }

    protected internal CommandResult(T value, bool success, Error error) : base(success, error)
    {
        _value = value;
    }
}
