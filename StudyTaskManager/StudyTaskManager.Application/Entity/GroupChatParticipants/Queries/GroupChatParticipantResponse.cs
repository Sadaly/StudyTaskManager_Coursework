using StudyTaskManager.Domain.Entity.Group.Chat;

namespace StudyTaskManager.Application.Entity.GroupChatParticipants.Queries;

public sealed record GroupChatParticipantResponse(
    Guid UserId,
    Guid GroupChatId)
{
    internal GroupChatParticipantResponse(GroupChatParticipant participant)
        : this(
             participant.UserId,
             participant.GroupChatId)
    { }
}
