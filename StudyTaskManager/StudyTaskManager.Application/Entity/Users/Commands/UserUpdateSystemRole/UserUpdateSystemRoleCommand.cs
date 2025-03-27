using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.User.Commands.UserUpdateSystemRole;
public sealed record UserUpdateSystemRoleCommand(
	Guid UserId,
	Guid SystemRoleId) : ICommand<Guid>;