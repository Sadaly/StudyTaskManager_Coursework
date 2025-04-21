using StudyTaskManager.Domain.Entity.Group;

namespace StudyTaskManager.Application.Entity.GroupInvites.Queries.GroupInviteGetByGroupAndUser;

public sealed record GroupInviteGetByGroupAndUserResponse(
    Guid SenderId,
    DateTime DateInvitation,
    bool? InvitationAccepted)
{
    internal GroupInviteGetByGroupAndUserResponse(GroupInvite groupInvite)
        : this(groupInvite.SenderId, groupInvite.DateInvitation, groupInvite.InvitationAccepted)
    { }
}
