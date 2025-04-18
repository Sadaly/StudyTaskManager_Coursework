using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.Users.Commands.UserVerifyPhoneNumber;

public sealed record UserVerifyPhoneNumberCommand(
	Guid UserId) : ICommand;

