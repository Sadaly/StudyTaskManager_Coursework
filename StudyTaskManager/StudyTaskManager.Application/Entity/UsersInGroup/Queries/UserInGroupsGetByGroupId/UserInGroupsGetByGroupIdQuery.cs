using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Queries.UserInGroupsGetByGroupId;

public sealed record UserInGroupsGetByGroupIdQuery(
    Guid GroupId,
    int StartIndex,
    int Count) : IQuery<List<UserInGroupsGetByGroupIdResponseElements>>;
