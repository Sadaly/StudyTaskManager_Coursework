using StudyTaskManager.Application.Abstractions.Messaging;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Queries.UserInGroupGetByUserAndGroupIds;

public sealed record UserInGroupGetByUserAndGroupIdsQuery(
    Guid UserId, 
    Guid GroupId) : IQuery<UserInGroupGetByUserAndGroupIdsResponse>;