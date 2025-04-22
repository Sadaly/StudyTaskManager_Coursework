using StudyTaskManager.Domain.Entity.User;

namespace StudyTaskManager.Application.Entity.SystemRoles.Queries;
public sealed record SystemRoleGetByIdResponse(
    string Name,
    bool CanViewPeoplesGroups,
    bool CanChangeSystemRoles,
    bool CanBlockUsers,
    bool CanDeleteChats)
{
    internal SystemRoleGetByIdResponse(SystemRole role)
        : this(
            role.Name.Value,
            role.CanViewPeoplesGroups,
            role.CanChangeSystemRoles,
            role.CanBlockUsers,
            role.CanDeleteChats)
    { }
}