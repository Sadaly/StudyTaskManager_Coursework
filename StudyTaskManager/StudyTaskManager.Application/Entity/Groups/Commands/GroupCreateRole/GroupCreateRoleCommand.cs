using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.Groups.Commands.GroupCreateRole;

public sealed record GroupCreateRoleCommand(
    Guid GroupId,
    string RoleName,
    bool CanCreateTasks,
    bool CanManageRoles,
    bool CanCreateTaskUpdates,
    bool CanChangeTaskUpdates,
    bool CanInviteUsers) : ICommand<Guid>;