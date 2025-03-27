using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.Users.Commands.UserChangeUsername;

public sealed record UserChangeUsernameCommand(
    Guid UserId,
    string NewUsername) : ICommand<Guid>;
