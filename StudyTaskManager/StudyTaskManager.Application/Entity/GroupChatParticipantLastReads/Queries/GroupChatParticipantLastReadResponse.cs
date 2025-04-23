using StudyTaskManager.Domain.Entity.Group.Chat;

namespace StudyTaskManager.Application.Entity.GroupChatParticipantLastReads.Queries
{
    public sealed record GroupChatParticipantLastReadResponse(
    Guid GroupChatId,
    Guid UserId
    )
    {
        internal GroupChatParticipantLastReadResponse(GroupChatParticipant gcp)
            : this(gcp.GroupChatId, gcp.UserId)
        { }
    }
}