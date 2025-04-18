using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.Users.Commands.UserChangePhoneNumber;
public sealed record UserChangePhoneNumberCommand(
    Guid UserId,
    string NewPhoneNumber) : ICommand;
