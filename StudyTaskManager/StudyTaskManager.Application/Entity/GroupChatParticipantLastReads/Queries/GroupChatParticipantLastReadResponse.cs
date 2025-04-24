using StudyTaskManager.Domain.Entity.Group.Chat;

namespace StudyTaskManager.Application.Entity.GroupChatParticipantLastReads.Queries
{
    public sealed record GroupChatParticipantLastReadResponse(
    Guid GroupChatId,
    Guid UserId,
    ulong LastRead
    )
    {
        internal GroupChatParticipantLastReadResponse(GroupChatParticipantLastRead gcplr)
            : this(gcplr.GroupChatId, gcplr.UserId, gcplr.LastReadMessageId)
        { }
    }
}