using ComicManagerClean.Application.User.Commands;
using FluentValidation;

namespace ComicManagerClean.Application.User.Validators;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(login => login.Email)
            .NotEmpty()
            .NotNull();

        RuleFor(login => login.Password)
            .NotEmpty()
            .NotNull();
    }
}
