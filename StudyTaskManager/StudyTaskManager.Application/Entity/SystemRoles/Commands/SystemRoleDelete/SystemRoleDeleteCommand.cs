using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.SystemRoles.Commands.SystemRoleDelete;

public sealed record SystemRoleDeleteCommand(
    Guid SystemRoleId
    ) : ICommand;
