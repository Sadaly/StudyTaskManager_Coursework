using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.SystemRoles.Commands.SystemRoleUpdatePrivileges;

public sealed record SystemRoleUpdatePrivilegesCommand(
    Guid SystemRoleId,
    bool CanViewPeoplesGroups,
    bool CanChangeSystemRoles,
    bool CanBlockUsers,
    bool CanDeleteChats) : ICommand;