using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.BlockedUserInfos.Queries.BlockedUserInfoGetAll;
public sealed record BlockedUserInfoGetAllQuery : IQuery<BlockedUserInfoListResponse>;
