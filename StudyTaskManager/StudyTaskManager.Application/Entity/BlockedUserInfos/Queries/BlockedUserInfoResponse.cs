using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Application.Entity.BlockedUserInfos.Queries;
public sealed record BlockedUserInfoResponse(BlockedUserInfo BlockedUserInfo);
public sealed record BlockedUserInfoListResponse(List<BlockedUserInfo> BlockedUserInfo);