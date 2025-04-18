using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.BlockedUserInfos.Commands.BlockedUserInfoDelete;
public sealed record BlockedUserInfoDeleteCommand(Guid UserId) : ICommand;