using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Queries.UserInGroupsGetByUserId
{
    public sealed record UserInGroupsGetByUserIdResponseElements
    {
        public Guid GroupId { get; init; }
        public Guid RoleId { get; init; }
        public DateTime DateEntered { get; init; }

        internal UserInGroupsGetByUserIdResponseElements(UserInGroup userInGroup)
        {
            GroupId = userInGroup.GroupId;
            RoleId = userInGroup.RoleId;
            DateEntered = userInGroup.DateEntered;
        }
    }
}
