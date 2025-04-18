using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.GroupRoles.Commands.GroupRoleUpdatePermissions;

public sealed record GroupRoleUpdatePermissionsCommand(
    Guid Id,
    bool CanCreateTasks,
    bool CanManageRoles,
    bool CanCreateTaskUpdates,
    bool CanChangeTaskUpdates,
    bool CanInviteUsers) : ICommand;}
