using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Queries.UserInGroupsGetByUserId;

public sealed record UserInGroupsGetByUserIdResponseElements(
    Guid GroupId,
    Guid RoleId,
    DateTime DateEntered)
{
    internal UserInGroupsGetByUserIdResponseElements(UserInGroup userInGroup)
        : this(userInGroup.GroupId, userInGroup.RoleId, userInGroup.DateEntered) { }
}
