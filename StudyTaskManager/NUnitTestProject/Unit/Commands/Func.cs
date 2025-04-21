using MediatR;
using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Shared;

namespace NUnitTestProject.Unit.Commands
{
    public static class Func
    {
        public static Result Processing(
            ICommand command,
            ICommandHandler<ICommand> handler)
        {
            return handler.Handle(command, CancellationToken.None).GetAwaiter().GetResult();
        }
        public static Result<TResponse> Processing<TCommand, TResponse>(
            TCommand command,
            ICommandHandler<TCommand, TResponse> handler)
            where TCommand : ICommand<TResponse>
        {
            return handler.Handle(command, CancellationToken.None).GetAwaiter().GetResult();
        }
    }
}