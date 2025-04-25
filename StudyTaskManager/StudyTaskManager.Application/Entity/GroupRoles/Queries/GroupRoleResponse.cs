using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Application.Entity.GroupRoles.Queries
{
    public sealed record GroupRoleResponse(
        Guid Id,
        string Title,
        bool CanCreateTasks,
        bool CanManageRoles,
        bool CanCreateTaskUpdates,
        bool CanChangeTaskUpdates,
        bool CanInviteUsers,
        Guid? GroupId)
    {
        internal GroupRoleResponse(GroupRole role)
            : this(role.Id, role.Title.Value, role.CanCreateTasks, role.CanManageRoles, role.CanCreateTaskUpdates, role.CanChangeTaskUpdates, role.CanInviteUsers, role.GroupId)
        { }
    }
}
