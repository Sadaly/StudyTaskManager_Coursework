using StudyTaskManager.Domain.Entity.User.Chat;

namespace StudyTaskManager.Application.Entity.PersonalChats.Queries;

public sealed record PersonalChatsResponse(
    Guid ChatId,
    Guid User1Id,
    Guid User2Id)
{
    internal PersonalChatsResponse(PersonalChat chat)
        : this(
              chat.Id,
              chat.User1Id,
              chat.User2Id)
    { }
}