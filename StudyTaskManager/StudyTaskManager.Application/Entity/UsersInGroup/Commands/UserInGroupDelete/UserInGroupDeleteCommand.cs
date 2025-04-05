using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Shared;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Commands.UserInGroupDelete;

public sealed record UserInGroupDeleteCommand(
    Guid UserId,
    Guid GroupId) : ICommand;
