using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.Groups.Commands.GroupRemoveUser;

public sealed record GroupRemoveUserCommand(
    Guid GroupId,
    Guid UserId) : ICommand;