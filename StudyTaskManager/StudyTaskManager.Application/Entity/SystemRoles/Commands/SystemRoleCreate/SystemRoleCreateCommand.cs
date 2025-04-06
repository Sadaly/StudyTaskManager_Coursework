using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Application.Entity.SystemRoles.Commands.SystemRoleCreate;

public sealed record SystemRoleCreateCommand(
    string Name,
    bool CanViewPeoplesGroups,
    bool CanChangeSystemRoles,
    bool CanBlockUsers,
    bool CanDeleteChats
    ) : ICommand<Guid>;
