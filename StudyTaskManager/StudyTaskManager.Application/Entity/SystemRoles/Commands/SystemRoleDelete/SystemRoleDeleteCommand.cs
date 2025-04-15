using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Application.Entity.Generic.Commands.DeleteById;
using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Application.Entity.SystemRoles.Commands.SystemRoleDelete;

public sealed record SystemRoleDeleteCommand(
    Guid SystemRoleId
    ) : DeleteByIdCommand<SystemRole>(SystemRoleId);
