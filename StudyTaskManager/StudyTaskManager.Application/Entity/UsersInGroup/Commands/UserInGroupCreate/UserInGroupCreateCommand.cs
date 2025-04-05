using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Commands.UserInGroupCreate;

public sealed record UserInGroupCreateCommand(
    Guid UserId,
    Guid GroupId,
    Guid RoleId
    ) : ICommand<UserInGroup>;
