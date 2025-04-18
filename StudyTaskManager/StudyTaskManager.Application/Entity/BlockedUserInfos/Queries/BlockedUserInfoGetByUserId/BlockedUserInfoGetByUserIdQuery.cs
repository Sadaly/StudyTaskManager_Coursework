using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.BlockedUserInfos.Queries.BlockedUserInfoGetByUserId;
public sealed record BlockedUserInfoGetByUserIdQuery(Guid UserId) : IQuery<BlockedUserInfoResponse>;
