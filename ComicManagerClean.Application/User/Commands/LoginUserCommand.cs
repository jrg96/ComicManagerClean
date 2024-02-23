using ComicManagerClean.Application.Abstractions;
namespace ComicManagerClean.Application.User.Commands;

public record LoginUserCommand(
    string Email
    , string Password
) : ICommand;
