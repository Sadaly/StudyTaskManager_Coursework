using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Commands.UserInGroupDelete;

public sealed record UserInGroupDeleteCommand(
    Guid UserId,
    Guid GroupId) : ICommand;
