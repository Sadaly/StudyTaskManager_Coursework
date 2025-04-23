using StudyTaskManager.Domain.Entity.Group.Chat;

namespace StudyTaskManager.Application.Entity.GroupChatMessages.Queries;
public sealed record GroupChatMessageResponse(
    Guid GroupChatId,
    Guid SenderId,
    ulong Ordinal,
    string Content,
    DateTime DateTime
    )
{
    internal GroupChatMessageResponse(GroupChatMessage gcm)
        : this(gcm.GroupChatId, gcm.SenderId, gcm.Ordinal, gcm.Content.Value, gcm.DateTime)
    { }
}