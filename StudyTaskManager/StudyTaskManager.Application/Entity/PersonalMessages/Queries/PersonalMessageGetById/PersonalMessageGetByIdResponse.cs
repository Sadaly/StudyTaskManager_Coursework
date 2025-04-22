using StudyTaskManager.Domain.Entity.User.Chat;

namespace StudyTaskManager.Application.Entity.PersonalMessages.Queries.PersonalMessageGetById;
public sealed record PersonalMessageGetByIdResponse(
    Guid PersonalChatId,
    Guid SenderId,
    DateTime DateWriten,
    string Content,
    bool Is_Read_By_Other_User)
{
    internal PersonalMessageGetByIdResponse(PersonalMessage message)
        : this(
              message.PersonalChatId,
              message.SenderId,
              message.DateWriten,
              message.Content.Value,
              message.Is_Read_By_Other_User)
    { }
}
