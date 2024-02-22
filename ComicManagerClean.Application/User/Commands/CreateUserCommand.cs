using ComicManagerClean.Application.Abstractions;
namespace ComicManagerClean.Application.User.Commands;

public record CreateUserCommand(
    string Name,
    string LastName,
    string Email,
    string Password
) : ICommand;
