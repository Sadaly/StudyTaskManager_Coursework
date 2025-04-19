using StudyTaskManager.Domain.Entity.Group;
using static StudyTaskManager.Application.Entity.UsersInGroup.Queries.UserInGroupsGetByUserId.UserInGroupsGetByUserIdResponse;

namespace StudyTaskManager.Application.Entity.UsersInGroup.Queries.UserInGroupsGetByUserId
{
    public sealed record UserInGroupsGetByUserIdResponse(
        List<UserInGroupsGetByUserIdResponseElements> List)
    {
        public class UserInGroupsGetByUserIdResponseElements
        {
            public Guid GroupId { get; internal set; }
            public Guid RoleId { get; internal set; }
            public DateTime DateEntered { get; internal set; }

            internal UserInGroupsGetByUserIdResponseElements(UserInGroup userInGroup)
            {
                GroupId = userInGroup.GroupId;
                RoleId = userInGroup.RoleId;
                DateEntered = userInGroup.DateEntered;
            }
        }
    }
}
