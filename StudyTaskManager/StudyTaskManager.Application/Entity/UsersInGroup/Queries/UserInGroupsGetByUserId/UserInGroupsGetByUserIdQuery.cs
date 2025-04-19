using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Queries.UserInGroupsGetByUserId;

public sealed record UserInGroupsGetByUserIdQuery(
    Guid Id,
    int StartIndex,
    int Count) : IQuery<UserInGroupsGetByUserIdResponse>;