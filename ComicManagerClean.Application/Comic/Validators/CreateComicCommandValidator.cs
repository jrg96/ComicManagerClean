using ComicManagerClean.Application.Comic.Commands;
using FluentValidation;

namespace ComicManagerClean.Application.Comic.Validators;

public class CreateComicCommandValidator : AbstractValidator<CreateComicCommand>
{
    public CreateComicCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(200);

        RuleFor(command => command.ReleaseDate)
            .NotEmpty()
            .NotNull();

        RuleFor(command => command.Chapters)
            .GreaterThanOrEqualTo(1);
    }
}
