using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Queries.UserInGroupsGetByUserId;

public sealed record UserInGroupsGetByUserIdQuery(
    Guid UserId,
    int StartIndex,
    int Count) : IQuery<List<UserInGroupsResponse>>;