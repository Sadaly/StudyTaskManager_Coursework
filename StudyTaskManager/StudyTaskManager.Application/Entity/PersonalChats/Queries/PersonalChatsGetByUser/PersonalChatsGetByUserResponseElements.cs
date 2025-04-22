using StudyTaskManager.Domain.Entity.User.Chat;

namespace StudyTaskManager.Application.Entity.PersonalChats.Queries.PersonalChatsGetByUser;

public sealed record PersonalChatsGetByUserResponseElements(
    Guid ChatId,
    Guid OtherUser)
{
    internal PersonalChatsGetByUserResponseElements(PersonalChat chat, Guid UserIdFromRequest)
        : this(chat.Id,
             chat.User1Id == UserIdFromRequest ?
                chat.User1Id :
                chat.User2Id)
    { }
}