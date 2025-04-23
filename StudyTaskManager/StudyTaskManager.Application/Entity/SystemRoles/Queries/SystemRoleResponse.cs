using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Application.Entity.SystemRoles.Queries;
public sealed record SystemRoleResponse(
    string Name,
    bool CanViewPeoplesGroups,
    bool CanChangeSystemRoles,
    bool CanBlockUsers,
    bool CanDeleteChats)
{
    internal SystemRoleResponse(SystemRole role)
        : this(
            role.Name.Value,
            role.CanViewPeoplesGroups,
            role.CanChangeSystemRoles,
            role.CanBlockUsers,
            role.CanDeleteChats)
    { }
}