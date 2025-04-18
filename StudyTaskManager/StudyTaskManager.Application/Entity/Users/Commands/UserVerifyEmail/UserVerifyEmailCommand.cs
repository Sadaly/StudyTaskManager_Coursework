using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.Users.Commands.UserVerifyEmail;

public sealed record UserVerifyEmailCommand(
	Guid UserId) : ICommand;