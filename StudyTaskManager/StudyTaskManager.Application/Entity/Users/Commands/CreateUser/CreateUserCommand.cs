using StudyTaskManager.Application.Abstractions.Messaging;
using StudyTaskManager.Domain.Entity.User;
using StudyTaskManager.Domain.ValueObjects;

namespace StudyTaskManager.Application.Entity.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    UserName UserName, 
    Email Email, 
    Password Password, 
    PhoneNumber? PhoneNumber,
    SystemRole? SystemRoleId) : ICommand<Guid>;
