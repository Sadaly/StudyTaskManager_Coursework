using StudyTaskManager.Application.Abstractions.Messaging;
namespace StudyTaskManager.Application.Entity.Users.Commands.UserUpdatePassword;

public sealed record UserUpdatePasswordCommand(
	Guid UserId,
	string NewPassword) : ICommand;
