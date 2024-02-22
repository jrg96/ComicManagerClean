namespace ComicManagerClean.Domain.Shared;

public class Error
{
    // Static values
    public static readonly Error None = new Error(string.Empty, string.Empty);

    public string Code { get; }
    public string Message { get; }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }
}
