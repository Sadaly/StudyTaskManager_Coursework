using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.Users.Commands.UserCreate;

public sealed record UserCreateCommand(
    string Username,
    string Email,
    string Password,
    string? PhoneNumber,
    Guid? SystemRoleId) : ICommand<Guid>;