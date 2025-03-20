using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    string UserName, 
    string Email, 
    string Password, 
    string? PhoneNumber,
    SystemRole? SystemRole) : ICommand<Guid>;
