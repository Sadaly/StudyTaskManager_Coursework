using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.Users.Commands.CreateUser;

public sealed record CreateUserCommand(string UserName, int SystemRoleId, string? Email, string? NumberPhone) : ICommand<Guid>;
