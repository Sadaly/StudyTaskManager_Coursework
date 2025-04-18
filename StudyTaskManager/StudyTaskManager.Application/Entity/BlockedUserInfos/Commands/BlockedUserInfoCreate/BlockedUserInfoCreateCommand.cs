using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.BlockedUserInfos.Commands.BlockedUserInfoCreate;
public sealed record BlockedUserInfoCreateCommand(Guid UserId, string Reason) : ICommand<Guid>;
