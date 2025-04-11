using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Commands.UserInGroupUpdateRole;

public sealed record UserInGroupUpdateRoleCommand(
    Guid UserId,
    Guid GroupId,
    Guid NewGroupRoleId) : ICommand;