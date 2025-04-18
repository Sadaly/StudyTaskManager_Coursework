
using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupRoles.Commands.GroupRoleDelete;

public sealed record GroupRoleDeleteCommand(Guid Id) : ICommand;