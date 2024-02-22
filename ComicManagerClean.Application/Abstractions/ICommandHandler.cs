using ComicManagerClean.Domain.Shared;
using MediatR;

namespace ComicManagerClean.Application.Abstractions;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, CommandResult> 
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, CommandResult<TResponse>>
    where TCommand : ICommand<TResponse>
    where TResponse : class
{
}
