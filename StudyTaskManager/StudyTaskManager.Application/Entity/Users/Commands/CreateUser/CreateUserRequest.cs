namespace StudyTaskManager.Application.Entity.Users.Commands.CreateUser;

public sealed record CreateUserRequest(string UserName, int SystemRoleId, string? Email, string? NumberPhone);
