using ComicManagerClean.Application.Comic.Commands;
using FluentValidation;

namespace ComicManagerClean.Application.Comic.Validators;

public class DeleteComicCommandValidator : AbstractValidator<DeleteComicCommand>
{
    public DeleteComicCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();
    }
}
