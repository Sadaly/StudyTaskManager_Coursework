using StudyTaskManager.Domain.Entity.Group.Chat;

namespace StudyTaskManager.Application.Entity.GroupChats.Queries;

public sealed record GroupChatResponse(
    Guid IdChat,
    Guid GroupId,
    string Name,
    bool IsPublic
    )
{
    internal GroupChatResponse(GroupChat chat)
        : this(
             chat.Id,
             chat.GroupId,
             chat.Name.Value,
             chat.IsPublic)
    { }
}
