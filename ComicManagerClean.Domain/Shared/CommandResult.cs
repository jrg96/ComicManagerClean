namespace ComicManagerClean.Domain.Shared;

public class CommandResult
{
    public bool IsSuccess { get; }
    public Error Error { get; }

    protected internal CommandResult(bool success, Error error)
    {
        IsSuccess = success;
        Error = error;
    }

    public static CommandResult Success()
    {
        return new CommandResult(true, Error.None);
    }
}
