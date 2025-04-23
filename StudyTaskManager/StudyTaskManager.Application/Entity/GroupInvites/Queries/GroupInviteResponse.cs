using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Application.Entity.GroupInvites.Queries;

public sealed record GroupInviteResponse(
    Guid SenderId,
    DateTime DateInvitation,
    bool? InvitationAccepted)
{
    internal GroupInviteResponse(GroupInvite groupInvite)
        : this(groupInvite.SenderId, groupInvite.DateInvitation, groupInvite.InvitationAccepted)
    { }
}
