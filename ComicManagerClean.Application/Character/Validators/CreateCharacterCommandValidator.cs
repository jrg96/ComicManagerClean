using ComicManagerClean.Application.Character.Commands;
using FluentValidation;

namespace ComicManagerClean.Application.Character.Validators;

public class CreateCharacterCommandValidator : AbstractValidator<CreateCharacterCommand>
{
    public CreateCharacterCommandValidator()
    {
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

        RuleFor(command => command.CharacterType)
            .NotEmpty();
    }
}
