using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.Users.Commands.UserUpdateSystemRole;
public sealed record UserUpdateSystemRoleCommand(
	Guid UserId,
	Guid SystemRoleId) : ICommand;