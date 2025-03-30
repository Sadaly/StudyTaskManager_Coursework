using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.Users.Commands.UserLogin;

public sealed record UserLoginCommand(string Email, string Password) : ICommand<string>;