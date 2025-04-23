using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Queries;

public sealed record UserInGroupsResponse(
    Guid UserId,
    Guid GroupId,
    Guid RoleId,
    DateTime DateEntered)
{
    internal UserInGroupsResponse(UserInGroup userInGroup)
        : this(userInGroup.UserId, userInGroup.RoleId, userInGroup.GroupId, userInGroup.DateEntered) { }
};