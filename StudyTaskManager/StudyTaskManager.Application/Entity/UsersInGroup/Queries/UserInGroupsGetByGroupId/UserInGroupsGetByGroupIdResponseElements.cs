using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Queries.UserInGroupsGetByGroupId;

public sealed record UserInGroupsGetByGroupIdResponseElements(
    Guid UserId,
    Guid RoleId,
    DateTime DateEntered)
{
    internal UserInGroupsGetByGroupIdResponseElements(UserInGroup userInGroup)
        : this(userInGroup.UserId, userInGroup.RoleId, userInGroup.DateEntered) { }
};