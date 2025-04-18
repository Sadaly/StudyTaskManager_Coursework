using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Queries;

public sealed record UserInGroupResponse(UserInGroup UserInGroup);
public sealed record UserInGroupListResponse(List<UserInGroup> UsersInGroup);
