using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.User.Commands.UserVerifyPhoneNumber;

public sealed record UserVerifyPhoneNumberCommand(
	Guid UserId) : ICommand<Guid>;

