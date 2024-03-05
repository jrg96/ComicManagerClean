using ComicManagerClean.Application.Character.Commands;
using FluentValidation;

namespace ComicManagerClean.Application.Character.Validators;

public class UpdateCharacterCommandValidator : AbstractValidator<UpdateCharacterCommand>
{
    public UpdateCharacterCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();

        RuleFor(command => command.HeroName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(command => command.FirstName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(command => command.LastName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(command => command.DateOfBirth)
            .NotEmpty();
    }
}
