using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.Groups.Commands.GroupRemoveRole;

public sealed record GroupRemoveRoleCommand(
    Guid GroupId,
    Guid RoleId) : ICommand;