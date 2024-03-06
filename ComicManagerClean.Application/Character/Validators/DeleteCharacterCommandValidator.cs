using ComicManagerClean.Application.Character.Commands;
using FluentValidation;

namespace ComicManagerClean.Application.Character.Validators;

public class DeleteCharacterCommandValidator : AbstractValidator<DeleteCharacterCommand>
{
    public DeleteCharacterCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();
    }
}
