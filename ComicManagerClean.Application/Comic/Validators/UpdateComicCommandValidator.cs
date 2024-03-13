using ComicManagerClean.Application.Comic.Commands;
using FluentValidation;

namespace ComicManagerClean.Application.Comic.Validators;

public class UpdateComicCommandValidator : AbstractValidator<UpdateComicCommand>
{
    public UpdateComicCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();

        RuleFor(command => command.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(command => command.ReleaseDate)
            .NotEmpty();

        RuleFor(command => command.Chapters)
            .GreaterThanOrEqualTo(1);
    }
}
