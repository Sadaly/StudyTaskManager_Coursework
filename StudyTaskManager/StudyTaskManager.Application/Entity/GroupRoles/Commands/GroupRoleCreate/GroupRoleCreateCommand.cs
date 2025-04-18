using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupRoles.Commands.GroupRoleCreate;

public sealed record GroupRoleCreateCommand(
    string RoleName,
    bool CanCreateTasks,
    bool CanManageRoles,
    bool CanCreateTaskUpdates,
    bool CanChangeTaskUpdates,
    bool CanInviteUsers) : ICommand<Guid>;