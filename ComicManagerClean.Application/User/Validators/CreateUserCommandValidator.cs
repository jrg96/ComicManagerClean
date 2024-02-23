using ComicManagerClean.Application.User.Commands;
using FluentValidation;
using System.Text.RegularExpressions;

namespace ComicManagerClean.Application.User.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty()
            .NotNull();

        RuleFor(command => command.Password)
            .NotEmpty()
            .NotNull()
            .Length(10, 32)
            .Must(IsValidPassword);

        RuleFor(command => command.Name)
            .NotEmpty()
            .NotNull();

        RuleFor(command => command.LastName)
            .NotEmpty()
            .NotNull();
            
    }

    private bool IsValidPassword(string password)
    {
        var lowercase = new Regex("[a-z]+");
        var uppercase = new Regex("[A-Z]+");
        var digit = new Regex("(\\d)+");
        var symbol = new Regex("(\\W)+");

        return (lowercase.IsMatch(password) 
            && uppercase.IsMatch(password) 
            && digit.IsMatch(password) 
            && symbol.IsMatch(password));
    }
}
