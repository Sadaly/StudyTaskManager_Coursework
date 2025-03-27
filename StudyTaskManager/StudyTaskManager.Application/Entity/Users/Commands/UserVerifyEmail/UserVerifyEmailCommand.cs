using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.User.Commands.UserVerifyEmail;

public sealed record UserVerifyEmailCommand(
	Guid UserId) : ICommand<Guid>;