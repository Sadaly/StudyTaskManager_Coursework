using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.Users.Commands.UserDelete;

public sealed record UserDeleteCommand(
    Guid UserId) : ICommand<Guid>;
