using StudyTaskManager.Domain.Entity.User.Chat;

namespace StudyTaskManager.Application.Entity.PersonalChats.Queries.PersonalChatGetById;

public sealed record PersonalChatGetByIdResponse(
    IEnumerable<Guid> UsersID)
{
    internal PersonalChatGetByIdResponse(PersonalChat chat)
        : this(chat.UsersID)
    { }
}