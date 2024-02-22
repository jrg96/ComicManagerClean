using ComicManagerClean.Domain.Shared;
using MediatR;

namespace ComicManagerClean.Application.Abstractions;

public interface ICommand : IRequest<CommandResult>
{
}

public interface ICommand<TResult> : IRequest<CommandResult<TResult>> where TResult : class
{
}
